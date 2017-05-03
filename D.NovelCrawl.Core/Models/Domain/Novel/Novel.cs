using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models.CrawlModel;
using D.NovelCrawl.Core.Models.Domain.CrawlUrl;
using D.NovelCrawl.Core.Models.DTO;
using D.Spider.Core.Interface;
using D.Util.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.Domain.Novel
{
    /// <summary>
    /// 小说领域模型
    /// </summary>
    internal class Novel
    {
        ILogger _logger;

        IUrlManager _urlManager;

        int _vipChapterNeedCrawlCount;
        IWebsiteProxy _website;

        /// <summary>
        /// 非官方的目录url
        /// </summary>
        List<CatalogUrl> _unofficialUrls = new List<CatalogUrl>();

        int VipChaperNeedCrawlCount
        {
            get
            {
                return _vipChapterNeedCrawlCount;
            }
            set
            {
                if (_vipChapterNeedCrawlCount <= 0 && value > 0)
                {
                    _logger.LogDebug("将所有非官方目录页面设置爬取");
                    foreach (var nu in _unofficialUrls)
                    {
                        nu.NeedCrawl = true;
                    }
                }
                else if (_vipChapterNeedCrawlCount > 0 && value <= 0)
                {
                    foreach (var nu in _unofficialUrls)
                    {
                        nu.NeedCrawl = false;
                    }
                }

                _vipChapterNeedCrawlCount = value;
            }
        }

        #region 对外属性
        /// <summary>
        /// 小说 GUID
        /// </summary>
        public Guid Uid { get; private set; }

        /// <summary>
        /// 小说名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; private set; }

        /// <summary>
        /// 卷信息
        /// </summary>
        public Dictionary<int, Volume> Volumes { get; private set; }

        /// <summary>
        /// 章节信息
        /// </summary>
        public Dictionary<Guid, Chapter> Chapters { get; private set; }

        /// <summary>
        /// 官网目录 Url
        /// </summary>
        public CatalogUrl OfficialUrl { get; private set; }
        #endregion

        public Novel(
            ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , IWebsiteProxy website)
        {
            _logger = loggerFactory.CreateLogger<Novel>();
            _urlManager = urlManager;

            Volumes = new Dictionary<int, Volume>();
            Chapters = new Dictionary<Guid, Chapter>();
            _vipChapterNeedCrawlCount = 0;

            _website = website;
        }

        /// <summary>
        /// 根据从个人网站上获取的小说信息更新爬虫持有的小说信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(NovelModel model)
        {
            Uid = model.Uid;
            Name = model.Name;
        }

        /// <summary>
        /// 根据个人网站上记录的目录信息与爬虫持有的目录信息进行对比，判断哪些内容章节需要重新爬取
        /// 爬虫记录的小说目录信息需要与个人网站记录的目录信息同步
        /// 防止爬取到的章节上传失败或者某个章节报错需要重新爬取
        /// 在爬虫初始化之后，爬虫运行一段时间之后才需要调用这个函数
        /// </summary>
        /// <param name="catalog"></param>
        public void UpdateCatalog(NovelCatalogModel catalog)
        {
            lock (this)
            {
                //将本地 卷 信息设置为未上传
                foreach (var v in Volumes.Values)
                {
                    v.Uploaded = false;
                }

                foreach (var v in catalog.Vs)
                {
                    if (!Volumes.ContainsKey(v.No))
                    {
                        var volume = new Volume();
                        volume.No = v.No;
                        volume.Name = v.Name;
                        volume.Uploaded = true;

                        //远程已经存在的 卷 信息，不需要在上传
                        Volumes.Add(v.No, volume);
                    }
                    else
                    {
                        Volumes[v.No].Uploaded = true;
                    }
                }

                foreach (var c in Chapters.Values)
                {
                    c.NeedCrawl = true;
                    c.Uploaded = false;
                }

                foreach (var c in catalog.Cs)
                {
                    if (!Chapters.ContainsKey(c.Uid))
                    {
                        var chapter = new Chapter();
                        chapter.Uid = c.Uid;
                        chapter.Name = c.Name;
                        chapter.PublishTime = c.PublishTime;
                        chapter.NeedCrawl = c.NeedCrawl;
                        chapter.SourceUrl = "";
                        chapter.Uploaded = true;
                        chapter.Vip = c.Vip;
                        chapter.VolumeIndex = c.VolumeIndex;
                        chapter.VolumeNo = c.VolumeNo;
                        chapter.WordCount = c.WordCount;

                        Chapters.Add(c.Uid, chapter);
                    }
                    else
                    {
                        Chapters[c.Uid].NeedCrawl = c.NeedCrawl;
                        Chapters[c.Uid].Uploaded = true;
                    }

                    if (c.NeedCrawl && c.Vip)
                    {
                        VipChaperNeedCrawlCount++;
                    }
                }
            }
        }

        /// <summary>
        /// 设置小说相关的需要爬取的 url
        /// </summary>
        /// <param name="urls"></param>
        public void SetRelatedUrls(IEnumerable<NovelCrawlUrlModel> urls)
        {
            var official = urls
                .Where(uu => uu.Official)
                .FirstOrDefault();

            if (official == null)
            {
                _logger.LogWarning(Name + " 没有设置对应的官网目录页面，不对其进行爬取");
                return;
            }

            if (OfficialUrl == null ||
                OfficialUrl.String != official.Url)
            {
                if (OfficialUrl != null)
                    OfficialUrl.Interval = -1;

                OfficialUrl = CreateCatalogUrl(official.Url, true);

                _urlManager.AddUrl(OfficialUrl);
            }
            else
            {
                OfficialUrl.NeedCrawl = true;
            }

            var nf = urls.Where(u => u.Official == false);

            _unofficialUrls.Clear();

            foreach (var n in nf)
            {
                var url = CreateCatalogUrl(n.Url, false);

                _urlManager.AddUrl(url);
                _unofficialUrls.Add(url);
            }
        }

        /// <summary>
        /// 将从官网获取到的目录信息与持有的信息对比，确定需要爬取的章节信息
        /// </summary>
        /// <param name="crawledVolumes"></param>
        public void CmpareOfficialCatalog(CrawlVolumeModel[] crawledVolumes)
        {
            _logger.LogInformation("共获得卷信息：" + crawledVolumes.Length);

            for (var i = 0; i < crawledVolumes.Length; i++)
            {
                var cv = crawledVolumes[i];
                Volume v;

                if (!Volumes.ContainsKey(i + 1))
                {
                    v = new Volume()
                    {
                        No = i + 1,
                        Name = cv.Name.Replace(" ", ""),
                        Uploaded = false
                    };

                    Volumes.Add(v.No, v);

                    _website.UploadVolume(Uid, v);
                }
                else
                {
                    v = Volumes[i + 1];
                }

                for (var j = 0; j < cv.Chapters.Length; j++)
                {
                    var cc = cv.Chapters[j];
                    int index = j + 1;
                    Chapter c;

                    c = Chapters.Values
                        .Where(ccc => ccc.VolumeNo == v.No && ccc.VolumeIndex == index)
                        .FirstOrDefault();

                    if (c == null)
                    {
                        c = new Chapter();

                        c.Uid = Guid.NewGuid();
                        c.Name = cc.Name;
                        c.VolumeNo = v.No;
                        c.VolumeIndex = index;
                        c.PublishTime = StringToDateTime(cc.PublicTime);
                        c.NeedCrawl = true;
                        c.Vip = string.IsNullOrEmpty(cc.Vip) ? false : true;
                        c.WordCount = StringToInt(cc.WordCount);

                        Chapters.Add(c.Uid, c);

                        _website.UploadChapter(Uid, c);

                        if (c.NeedCrawl && c.Vip)
                        {
                            VipChaperNeedCrawlCount++;
                        }
                    }
                    if (c.NeedCrawl && !c.Vip)
                    {
                        //如果需要爬取的章节不是 vip 章节，直接从官网获取章节的内容信息
                        var urlStr = OfficialUrl.CreateCompleteUrl(cc.Href).String;
                        var url = CreateChapterTxtUrl(urlStr, c);
                        var inManager = _urlManager.AddUrl(url);
                    }
                }
            }
        }

        /// <summary>
        /// 从非官网获取的目录信息与持有的信息对比
        /// </summary>
        /// <param name="unoff"></param>
        /// <param name="crawledVolumes"></param>
        public void CmpareUnofficialCatalog(IUrl unoff, CrawlVolumeModel[] crawledVolumes)
        {
            foreach (var v in crawledVolumes)
            {
                foreach (var c in v.Chapters)
                {
                    var rs = Chapters.Values
                        .Where(cc => cc.Vip && IsNameEaual(cc.Name, c.Name));

                    if (rs.Count() == 0)
                    {
                        _logger.LogWarning("《{0}》官网中没有记录对应的章节：{1}", Name, c.Name);
                    }
                    else if (rs.Count() > 1)
                    {
                        _logger.LogWarning("《{0}》官网中有 {1} 记录对应的章节：{2}", Name, rs.Count(), c.Name);
                    }
                    else
                    {
                        var r = rs.FirstOrDefault();

                        if (r.Vip && r.NeedCrawl)
                        {
                            //如果需要爬取的章节不是 vip 章节，直接从官网获取章节的内容信息
                            var urlStr = unoff.CreateCompleteUrl(c.Href).String;
                            var url = CreateChapterTxtUrl(urlStr, r);
                            var inManager = _urlManager.AddUrl(url);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理从小说章节正文页面爬取到的章节正文信息
        /// 并且对爬取到的小说内容进行一些处理
        /// </summary>
        /// <param name="chapter"></param>
        public void DealChapterCrwalData(ChapterTxtUrl url, CrawlChapterModel crawlData)
        {
            var chapter = url.ChapterInfo;

            lock (this)
            {
                //章节已经不需要在爬取
                if (!chapter.NeedCrawl) return;
            }

            //1.去掉 html 标签
            var txt = RemoveHtmlTag(crawlData.Text);
            //2.判断字数

            //3.切分段落
            var pArray = txt.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //4.去除段落前后的空格
            for (var i = 0; i < pArray.Length; i++)
            {
                pArray[i] = pArray[i].Trim();
            }

            chapter.Text = JsonConvert.SerializeObject(pArray);

            lock (this)
            {
                chapter.NeedCrawl = false;
                chapter.SourceUrl = url.String;

                if (chapter.Vip) _vipChapterNeedCrawlCount--;
            }

            //3.上传到个人网站
            _website.UploadChapterText(chapter);
        }

        /// <summary>
        /// 去除小说正文中的html标签
        /// 将 <br/> 替换为 \n
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string RemoveHtmlTag(string txt)
        {
            var tmp = Regex.Replace(txt, @"<br[^>]*>", "\n", RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            tmp = tmp
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("&nbsp;", "");
            //.Replace("\r\n", "\n");

            return tmp;
        }

        /// <summary>
        /// 判断章节名是否相等
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static bool IsNameEaual(string n1, string n2)
        {
            return PreprocessName(n1) == PreprocessName(n2);
        }

        /// <summary>
        /// 对小说名进行预处理
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string PreprocessName(string name)
        {
            var array = name.Split(' ');
            name = array[array.Length - 1];
            name = Regex.Replace(name, @"[^\u4e00-\u9fa5,a-z,A-Z]", "");

            return name;
        }

        /// <summary>
        /// 创建一个新的小说目录 url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private CatalogUrl CreateCatalogUrl(string url, bool official)
        {
            if (official)
                return new CatalogUrl(url, _website, PageType.NovelCatalog)
                {
                    NovelInfo = this,
                    Official = true,
                    Interval = 1800,
                    NeedCrawl = true
                };
            else
                return new CatalogUrl(url, _website, PageType.NovelCatalog)
                {
                    NovelInfo = this,
                    Official = false,
                    Interval = 300,
                    NeedCrawl = false
                };
        }

        /// <summary>
        /// 创建一个新的小说章节正文 url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private ChapterTxtUrl CreateChapterTxtUrl(string url, Chapter chapter)
        {
            return new ChapterTxtUrl(url, _website, PageType.NovelChatperContext)
            {
                NovelInfo = this,
                Interval = -1,
                NeedCrawl = true,
                ChapterInfo = chapter
            };
        }

        private int StringToInt(string v)
        {
            try
            {
                return Convert.ToInt32(v);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 将字符串转换成 DateTime
        /// 用于对爬取到的小说章节发布时间做处理
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private DateTime StringToDateTime(string v)
        {
            var str = Regex.Replace(v, @"[^\d]", "");

            if (str.Length == 14)//20150911191411
            {
                try
                {
                    return DateTime.ParseExact(str, "yyyyMMddHHmmss", null);
                }
                catch
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }

        }
    }
}
