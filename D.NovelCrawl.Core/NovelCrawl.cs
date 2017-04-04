using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using D.Util.Interface;
using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Interface;
using Microsoft.Practices.Unity;
using D.NovelCrawl.Core.Models.CrawlModel;
using System.Timers;
using D.NovelCrawl.Core.Models.Domain.Novel;
using D.NovelCrawl.Core.Models.Domain.CrawlUrl;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬虫使用的 UrlManager 负责和个人网站交互
    /// 实现两个接口  IPageProcess
    /// </summary>
    public class NovelCrawl : INvoelCrawl, IPageProcess
    {
        ILogger _logger;
        IUnityContainer _container;

        ISpiderscriptEngine _spiderscriptEngine;

        IUrlManager _urlManager;

        IWebsiteProxy _web;

        /// <summary>
        /// 定时从官网获取小说以及对应的 url 信息，查看是否需要更新
        /// </summary>
        Timer _checkTimer;

        /// <summary>
        /// 定时器时间间隔
        /// 单位 秒
        /// </summary>
        const int _checkInterval = 3600;

        /// <summary>
        /// 所有的小说信息
        /// </summary>
        Dictionary<Guid, Novel> _novels = new Dictionary<Guid, Novel>();

        public NovelCrawl(
            ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , IWebsiteProxy webProxy
            , ISpiderscriptEngine spiderscriptEngine)
        {
            _logger = loggerFactory.CreateLogger<NovelCrawl>();
            _spiderscriptEngine = spiderscriptEngine;

            _urlManager = urlManager;

            _web = webProxy;
        }

        #region INvoelCrawl 实现
        public INvoelCrawl Initialization(IUnityContainer container)
        {
            _container = container;
            return this;
        }

        public INvoelCrawl Run()
        {
            CheckNovel();

            _checkTimer = new Timer(_checkInterval * 1000);
            _checkTimer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) =>
            {
                CheckNovel();
            });

            _checkTimer.Start();

            return this;
        }

        private void CheckNovel()
        {
            var result = _web.NovelList();

            foreach (var n in result.CurrPageData)
            {
                Novel novel;

                if (_novels.ContainsKey(n.Uid))
                {
                    novel = _novels[n.Uid];
                }
                else
                {
                    _logger.LogInformation("添加新的需要爬取的小说：" + n.Name);

                    novel = _container.Resolve<Novel>();
                }

                //更新小说基本信息
                novel.Update(n);

                //更新小说对应的小说爬取目录
                var urls = _web.NovelCrawlUrls(novel.Uid);
                novel.SetRelatedUrls(urls);

                //获取已经爬取到的小说目录信息，与爬虫本地持有的信息进行对比
                var catalog = _web.NovelCatalog(n.Uid);
                novel.UpdateCatalog(catalog);
            }
        }
        #endregion

        #region IPageProcess 实现
        public void Process(IUrl url)
        {
            var crawlUrl = url as NovelCrawlUrl;

            if (crawlUrl.PaseCode == null)
            {
                _logger.LogWarning("{0} 没有对应的处理代码", url.String);
                return;
            }


            if (!UrlDataDownloadComplete(crawlUrl))
            {
                _urlManager.RecrawlUrl(url);
                return;
            }

            switch (crawlUrl.PageType)
            {
                case PageType.NovelCatalog: NovleCatalogPage(url as CatalogUrl); break;
                case PageType.NovelChatperContext: NovleChapterTxtPage(url as ChapterTxtUrl); break;
            }
        }

        /// <summary>
        /// 处理小说目录页面
        /// </summary>
        /// <param name="page"></param>
        private void NovleCatalogPage(CatalogUrl url)
        {
            try
            {
                var data = _spiderscriptEngine.Run(url.Page.HtmlTxt, url.PaseCode.SSCriptCode);

                //_logger.LogDebug("{0} 分析到的数据：\r\n{1}", url.String, data.ToString());

                if (url.NovelInfo != null)
                {
                    var cd = data.ToObject<CrawlVolumeModel[]>();

                    if (url.Official)
                        url.NovelInfo.CmpareOfficialCatalog(cd);
                    else
                        url.NovelInfo.CmpareUnofficialCatalog(url, cd);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }
        }

        private void NovleChapterTxtPage(ChapterTxtUrl url)
        {
            try
            {
                var data = _spiderscriptEngine.Run(url.Page.HtmlTxt, url.PaseCode.SSCriptCode);

                //_logger.LogDebug("{0} 分析到的数据：\r\n{1}", url.String, data.ToString());

                if (url.NovelInfo != null)
                {
                    var cm = data.ToObject<CrawlChapterModel>();

                    url.NovelInfo.DealChapterCrwalData(url, cm);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 判断一个 url 的数据是否下载完整
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool UrlDataDownloadComplete(NovelCrawlUrl url)
        {
            if (url.PaseCode != null && url.PaseCode.MinLength > 0
                 && url.PaseCode.MinLength > url.Page.HtmlTxt.Length)
            {
                _logger.LogWarning("{0} 页面下载的 html 数据长度 {1}，不足 {2}", url.String, url.Page.HtmlTxt.Length, 40000);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
