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
        /// 所有的小说信息
        /// </summary>
        Dictionary<Guid, Novel> _novels = new Dictionary<Guid, Novel>();

        /// <summary>
        /// url 与 Novel 的对应关系
        /// </summary>
        Dictionary<IUrl, Novel> _url2novel = new Dictionary<IUrl, Novel>();

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

        public INvoelCrawl Initialization(IUnityContainer container)
        {
            _container = container;
            return this;
        }

        public INvoelCrawl Run()
        {
            //从网站上面获取需要爬取的小说
            var novels = _web.NovelList();

            if (novels.RecordCount <= 0)
            {
                _logger.LogWarning("没有需要爬取的小说");
            }
            else
            {
                foreach (var n in novels.CurrPageData)
                {
                    Novel dealNovel = null;

                    if (_novels.ContainsKey(n.Guid))
                    {
                        dealNovel = _novels[n.Guid];
                        dealNovel.Update(n);
                    }
                    else
                    {
                        dealNovel = new Novel();
                        dealNovel.Update(n);

                        _logger.LogInformation("添加需要爬取的小说：" + n.Name);
                        _novels.Add(dealNovel.Guid, dealNovel);
                    }

                    DealNovel(dealNovel);
                }
            }

            return this;
        }

        #region IPageProcess 实现
        public void Process(IPage page)
        {
            switch ((UrlTypes)page.Url.CustomType)
            {
                case UrlTypes.NovleCatalog: NovleCatalogPage(page); break;
                case UrlTypes.NovleChapterTxt: NovleChapterTxtPage(page); break;
            }
        }

        /// <summary>
        /// 处理小说目录页面
        /// </summary>
        /// <param name="page"></param>
        public void NovleCatalogPage(IPage page)
        {
            var code = _web.UrlPageProcessSpiderscriptCode(page.Url.Host, (UrlTypes)page.Url.CustomType);

            try
            {
                var data = _spiderscriptEngine.Run(page.HtmlTxt, code);

                _logger.LogInformation(data.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(page.Url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }
        }

        public void NovleChapterTxtPage(IPage page)
        {
            var step = _web.UrlPageProcessSpiderscriptCode(page.Url.Host, (UrlTypes)page.Url.CustomType);
        }
        #endregion

        /// <summary>
        /// 处理小说
        /// 是否需要重新获取信息，判断小说官网目录是否在 UrlManager 列表中
        /// </summary>
        /// <param name="deal"></param>
        private void DealNovel(Novel deal)
        {
            var urls = _web.NovelCrawlUrls(deal.Guid);
            if (urls != null && urls.Count() > 0)
            {
                var official = urls.Where(uu => uu.Official).FirstOrDefault();

                if (official == null)
                {
                    _logger.LogWarning(deal.Name + " 没有记录官网目录 url");
                }
                else
                {
                    if (deal.OfficialUrl == null || deal.OfficialUrl.String != official.Url)
                    {
                        deal.OfficialUrl = new Url(official.Url);

                        deal.OfficialUrl.Interval = 1800;
                        deal.OfficialUrl.CustomType = (int)UrlTypes.NovleCatalog;

                        _url2novel.Add(deal.OfficialUrl, deal);

                        _urlManager.AddUrl(deal.OfficialUrl);
                    }
                }
            }
            else
            {
                _logger.LogWarning(deal.Name + " 没有记录需要爬取的 Urls");
            }
        }
    }
}
