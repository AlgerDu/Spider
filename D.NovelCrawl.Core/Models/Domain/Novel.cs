using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models.CrawlModel;
using D.NovelCrawl.Core.Models.DTO;
using D.Spider.Core;
using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.Domain
{
    /// <summary>
    /// 小说爬虫需要的所有信息
    /// </summary>
    internal class Novel
    {
        ILogger _logger;

        IUrlManager _urlManager;

        int _vipChapterNeedCrawlCount;
        IWebsitProxy _web;

        /// <summary>
        /// 非官方的目录url
        /// </summary>
        List<IUrl> _unofficialUrls = new List<IUrl>();

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
        public Guid Uuid { get; private set; }

        /// <summary>
        /// 小说名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; private set; }

        /// <summary>
        /// 卷信息
        /// </summary>
        public Dictionary<int, VolumeModel> Volumes { get; private set; }

        /// <summary>
        /// 章节信息
        /// </summary>
        public Dictionary<Guid, ChapterModel> Chapters { get; private set; }

        /// <summary>
        /// 官网目录 Url
        /// </summary>
        public IUrl OfficialUrl { get; private set; }
        #endregion

        public Novel(
            ILoggerFactory loggerFactory,
            IUrlManager urlManager
            , IWebsitProxy web)
        {
            _logger = loggerFactory.CreateLogger<Novel>();
            _urlManager = urlManager;

            Volumes = new Dictionary<int, VolumeModel>();
            Chapters = new Dictionary<Guid, ChapterModel>();
            _vipChapterNeedCrawlCount = 0;

            _web = web;
        }

        /// <summary>
        /// 根据从个人网站上获取的小说信息更新爬虫持有的小说信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(NovelModel model)
        {
            Uuid = model.Uuid;
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
                        //远程已经存在的 卷 信息，不需要在上传
                        Volumes.Add(v.No, v);

                        v.Uploaded = true;
                    }
                    else
                    {
                        Volumes[v.No].Uploaded = true;
                    }
                }

                foreach (var c in Chapters.Values)
                {
                    c.Recrawl = true;
                }

                foreach (var c in catalog.Cs)
                {
                    if (!Chapters.ContainsKey(c.Uuid))
                    {
                        Chapters.Add(c.Uuid, c);

                        c.Recrawl = false;
                    }
                    else
                    {
                        Chapters[c.Uuid].Recrawl = c.Recrawl;
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

                OfficialUrl = new Url(official.Url);
                OfficialUrl.CustomData = new CatalogUrlData
                {
                    NovelInfo = this,
                    Official = true,
                    Type = UrlTypes.NovleCatalog
                };
                OfficialUrl.Interval = 1800;
                OfficialUrl.NeedCrawl = true;

                OfficialUrl = _urlManager.AddUrl(OfficialUrl);
            }
            else
            {
                OfficialUrl.NeedCrawl = true;
            }

            var nf = urls.Where(u => u.Official == false);

            _unofficialUrls.Clear();

            foreach (var n in nf)
            {
                var url = _urlManager.AddUrl(new Url(n.Url));

                url.CustomData = new CatalogUrlData
                {
                    NovelInfo = this,
                    Official = false,
                    Type = UrlTypes.NovleCatalog
                };
                url.Interval = 300;
                url.NeedCrawl = false;

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
                VolumeModel v;

                if (!Volumes.ContainsKey(i + 1))
                {
                    v = new VolumeModel()
                    {
                        No = i + 1,
                        Name = cv.Name
                    };

                    Volumes.Add(v.No, v);

                    _web.UploadNovelVolume(Uuid, v);

                    _logger.LogInformation("上传小说《{0}》未收录的卷：{1}", Name, v.Name);
                }
                else
                {
                    v = Volumes[i + 1];
                }

                for (var j = 0; j < cv.Chapters.Length; j++)
                {
                    var cc = cv.Chapters[j];
                    int index = j + 1;
                    ChapterModel c;

                    c = Chapters.Values
                        .Where(ccc => ccc.VolumeNo == v.No && ccc.VolumeIndex == index)
                        .FirstOrDefault();

                    if (c == null)
                    {
                        c = new ChapterModel();

                        c.Uuid = Guid.NewGuid();
                        c.Name = cc.Name;
                        c.VolumeNo = v.No;
                        c.VolumeIndex = index;
                        //PublicTime = DateTime.ParseExact(cc.PublicTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture),
                        c.Recrawl = true;
                        c.Vip = string.IsNullOrEmpty(cc.Vip) ? false : true;
                        //c.WordCount = Convert.ToInt32(cc.WordCount);

                        Chapters.Add(c.Uuid, c);
                    }

                    if (c.Recrawl && c.Vip)
                    {
                        VipChaperNeedCrawlCount++;
                    }
                    else if (c.Recrawl && !c.Vip)
                    {
                        //如果需要爬取的章节不是 vip 章节，直接从官网获取章节的内容信息
                        //IUrl url = OfficialUrl.CreateCompleteUrl(cc.Href);
                        //url.CustomData = new ChapterTxtUrlData
                        //{
                        //    NovelInfo = this,
                        //    Type = UrlTypes.NovleChapterTxt,
                        //    ChapterInfo = c
                        //};
                        //url.Interval = -1;

                        //var inManager = _urlManager.AddUrl(url);
                        //inManager.NeedCrawl = true;
                    }
                }
            }
        }

        public void CmpareUnofficialCatalog(IUrl unoff, CrawlVolumeModel[] crawledVolumes)
        {
            foreach (var v in crawledVolumes)
            {
                foreach (var c in v.Chapters)
                {
                    var r = Chapters.Values.Where(cc => cc.Name == c.Name).FirstOrDefault();

                    if (r == null)
                    {
                        _logger.LogWarning("《{0}》官网中没有记录对应的章节：{1}", Name, c.Name);
                    }
                    else
                    {
                        if (r.Vip && r.Recrawl)
                        {
                            IUrl url = unoff.CreateCompleteUrl(c.Href);
                            url.CustomData = new ChapterTxtUrlData
                            {
                                NovelInfo = this,
                                Type = UrlTypes.NovleChapterTxt,
                                ChapterInfo = r
                            };
                            url.Interval = -1;
                            url.NeedCrawl = true;

                            _urlManager.AddUrl(url);
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
        public void DealChapterCrwalData(ChapterModel chapter, CrawlChapterModel crawlData, string url)
        {
            lock (this)
            {
                //章节已经不需要在爬取
                if (!chapter.Recrawl) return;
            }

            //1.去掉 html 标签
            var txt = RemoveHtmlTag(crawlData.Text);
            //2.判断字数

            chapter.Text = txt;

            lock (this)
            {
                chapter.Recrawl = false;
                chapter.SourceUrl = url;

                if (chapter.Vip) _vipChapterNeedCrawlCount--;
            }

            //3.上传到个人网站
            _web.UploadNovelChapter(Uuid, chapter);
        }

        /// <summary>
        /// 去除小说正文中的html标签
        /// 将 <br/> 替换为 \r
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string RemoveHtmlTag(string txt)
        {
            var tmp = Regex.Replace(txt, @"<br[^>]*>", "\r", RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
            tmp = Regex.Replace(tmp, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            tmp = tmp
                .Replace(' ', '\0')
                .Replace('\t', '\0')
                .Replace("&nbsp;", "");

            return tmp;
        }
    }
}
