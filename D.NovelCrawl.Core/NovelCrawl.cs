using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using D.Util.Interface;
using D.Spider.Core;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using NSoup.Nodes;
using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models.DTO;
using Newtonsoft.Json;
using Microsoft.Practices.Unity;
using D.NovelCrawl.Core.Models.CrawlModel;
using System.Timers;
using D.NovelCrawl.Core.Models.Domain;

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

        IWebsitProxy _web;

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
            , IWebsitProxy webProxy
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

                if (_novels.ContainsKey(n.Uuid))
                {
                    novel = _novels[n.Uuid];
                }
                else
                {
                    _logger.LogInformation("添加新的需要爬取的小说：" + n.Name);

                    novel = _container.Resolve<Novel>();
                }

                //更新小说基本信息
                novel.Update(n);

                //获取已经爬取到的小说目录信息，与爬虫本地持有的信息进行对比
                var catalog = _web.NovelCatalog(n.Uuid);
                novel.UpdateCatalog(catalog);

                //更新小说对应的小说爬取目录
                var urls = _web.NovelCrawlUrls(novel.Uuid);
                novel.SetRelatedUrls(urls);
            }
        }
        #endregion

        #region IPageProcess 实现
        public void Process(IUrl url)
        {
            switch ((url.CustomData as UrlData).Type)
            {
                case UrlTypes.NovleCatalog: NovleCatalogPage(url); break;
                case UrlTypes.NovleChapterTxt: NovleChapterTxtPage(url); break;
            }
        }

        /// <summary>
        /// 处理小说目录页面
        /// </summary>
        /// <param name="page"></param>
        public void NovleCatalogPage(IUrl url)
        {
            var urlData = url.CustomData as CatalogUrlData;
            var code = _web.UrlPageProcessSpiderscriptCode(url.Host, urlData.Type);

            try
            {
                var data = _spiderscriptEngine.Run(url.Page.HtmlTxt, code);

                //_logger.LogDebug("{0} 分析到的数据：\r\n{1}", page.Url.String, data.ToString());

                if (urlData.NovelInfo != null)
                {
                    var cd = data.ToObject<CrawlVolumeModel[]>();

                    if (urlData.Official)
                        urlData.NovelInfo.CmpareOfficialCatalog(cd);
                    else
                        urlData.NovelInfo.CmpareUnofficialCatalog(url, cd);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }
        }

        public void NovleChapterTxtPage(IUrl url)
        {
            var urlData = url.CustomData as ChapterTxtUrlData;
            var code = _web.UrlPageProcessSpiderscriptCode(url.Host, urlData.Type);
            try
            {
                var data = _spiderscriptEngine.Run(url.Page.HtmlTxt, code);

                //_logger.LogDebug("{0} 分析到的数据：\r\n{1}", page.Url.String, data.ToString());

                if (urlData.NovelInfo != null)
                {
                    var cm = data.ToObject<CrawlChapterModel>();

                    urlData.NovelInfo.DealChapterCrwalData(urlData.ChapterInfo, cm, url.String);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }
        }
        #endregion
    }
}
