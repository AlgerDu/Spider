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

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬虫使用的 UrlManager 负责和个人网站交互
    /// 实现两个接口  IPageProcess
    /// </summary>
    public class NovelCrawl : INvoelCrawl, IPageProcess
    {
        ILogger _logger;

        IUrlManager _urlManager;

        IWebsitProxy _web;

        /// <summary>
        /// 所有的小说信息
        /// </summary>
        Dictionary<Guid, Novel> _novels = new Dictionary<Guid, Novel>();

        public NovelCrawl(
            ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , IWebsitProxy webProxy)
        {
            _logger = loggerFactory.CreateLogger<NovelCrawl>();

            _urlManager = urlManager;

            _web = webProxy;
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
            var step = _web.UrlPageProcess(page.Url.Host, (UrlTypes)page.Url.CustomType);

            Document doc = NSoup.NSoupClient.Parse(page.HtmlTxt);
            JToken data = new JObject();

            try
            {
                ExecuteStep(doc, step, ref data);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(page.Url.String + " 页面解析出现错误：" + ex.ToString());
                return;
            }

            _logger.LogInformation(data.ToString());
        }

        public void NovleChapterTxtPage(IPage page)
        {
            var step = _web.UrlPageProcess(page.Url.Host, (UrlTypes)page.Url.CustomType);
        }

        /// <summary>
        /// 解析页面获取数据
        /// </summary>
        /// <param name="step"></param>
        /// <param name="data"></param>
        public void ExecuteStep(Element ele, PageProcessStep step, ref JToken data)
        {
            if (step == null)
            {
                return;
            }

            if (step.Type == PageProcessStepTypes.Html)
            {
                var array = new JArray();
                var es = ele.Select(step.ProcessStr);

                foreach (var e in es)
                {
                    JToken item = new JObject();
                    ExecuteStep(e, step.NextProcessStep, ref item);
                    array.Add(item);
                }

                data[step.DataNames] = array;
            }
            else if (step.Type == PageProcessStepTypes.RegExp)
            {
                Regex regex = new Regex(step.ProcessStr);
                var m = regex.Matches(ele.OuterHtml())[0];
                foreach (var n in step.DataNames.Split(';'))
                {
                    data[n] = m.Groups[n].Value;
                }

                ExecuteStep(ele, step.NextProcessStep, ref data);
            }
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
