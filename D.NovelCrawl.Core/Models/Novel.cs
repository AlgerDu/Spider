using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Models.CrawlModel;
using D.NovelCrawl.Core.Models.DTO;
using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;

namespace D.NovelCrawl.Core.Models
{
    /// <summary>
    /// 小说爬虫需要的所有信息
    /// </summary>
    internal class Novel
    {
        ILogger _logger;

        IUrlManager _urlManager;

        #region 对外属性
        /// <summary>
        /// 小说 GUID
        /// </summary>
        public Guid Guid { get; private set; }

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
        /// 官网目录 Url
        /// </summary>
        public IUrl OfficialUrl { get; set; }
        #endregion

        public Novel(
            ILoggerFactory loggerFactory,
            IUrlManager urlManager)
        {
            _logger = loggerFactory.CreateLogger<Novel>();
            _urlManager = urlManager;

            Volumes = new Dictionary<int, Volume>();
        }

        /// <summary>
        /// 根据从个人网站上获取的小说信息更新爬虫持有的小说信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(NovelListModel model)
        {
            Guid = model.Guid;
            Name = model.Name;
        }

        /// <summary>
        /// 根据个人网站上记录的目录信息与爬虫持有的目录信息进行对比，判断哪些内容章节需要重新爬取
        /// 爬虫记录的小说目录信息需要与个人网站记录的目录信息同步
        /// 防止爬取到的章节上传失败或者某个章节报错需要重新爬取
        /// 在爬虫初始化之后，爬虫运行一段时间之后才需要调用这个函数
        /// </summary>
        /// <param name="volumes"></param>
        public void UpdateCatalog(NovelVolumeModel[] volumes)
        {
            lock (this)
            {
                Volumes.Clear();

                foreach (var v in volumes)
                {
                    var tv = new Volume()
                    {
                        Number = v.Number,
                        Name = v.Name
                    };

                    foreach (var c in v.Chapters)
                    {
                        var tc = new Chapter
                        {
                            ChapterNO = c.ChapterNO,
                            Name = c.Name,
                            Number = c.Number,
                            PublicTime = c.PublicTime,
                            ReCrawl = c.ReCrawl,
                            VipChapter = c.VipChapter,
                            WordCount = c.WordCount,
                            SourceUrl = c.SourceUrl,

                            VolumeNumber = tv.Number
                        };

                        tv.Chapters.Add(tc.Number, tc);
                    }

                    Volumes.Add(tv.Number, tv);
                }
            }
        }

        /// <summary>
        /// 将从官网获取到的目录信息与持有的信息对比，确定需要爬取的章节信息
        /// </summary>
        /// <param name="crawledVolumes"></param>
        public void CmpareOfficialCatalog(CrawlVolumeModel[] crawledVolumes)
        {

        }
    }
}
