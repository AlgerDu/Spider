using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;
using D.Spider.Core;
using D.NovelCrawl.Core.DTO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬虫使用的 UrlManager 负责和个人网站交互
    /// 实现两个接口 IUrlManager, IPageProcess
    /// </summary>
    public class NovelCrawl : IPageProcess
    {
        ILogger _logger;

        IUrlManager _urlManager;

        WebsiteProxy _websiteProxy;

        /// <summary>
        /// 所有的小说信息
        /// </summary>
        Dictionary<Guid, Novel> _novels = new Dictionary<Guid, Novel>();

        public NovelCrawl(
            ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , WebsiteProxy proxy)
        {
            _logger = loggerFactory.CreateLogger<NovelCrawl>();

            _urlManager = urlManager;

            _websiteProxy = proxy;
        }

        public void Run()
        {
            //从网站上面获取需要爬取的小说
            var novelList = _websiteProxy.NovelList();

            if (novelList.RecordCount <= 0)
            {
                _logger.LogWarning("没有需要爬取的小说");
            }
            else
            {
                foreach (var n in novelList.CurrPageData)
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
            var step = _websiteProxy.PageProcess(page.Url.Host, (UrlTypes)page.Url.CustomType);
            JObject data = new JObject();
            ExecuteStep(page.HtmlTxt, step, ref data);

        }

        public void NovleChapterTxtPage(IPage page)
        {
            var step = _websiteProxy.PageProcess(page.Url.Host, (UrlTypes)page.Url.CustomType);
        }

        /// <summary>
        /// 解析页面获取数据
        /// </summary>
        /// <param name="step"></param>
        /// <param name="data"></param>
        public void ExecuteStep(string html, PageProcessStep step, ref JObject data)
        {
            if (step.Type == PageProcessStepTypes.Html)
            {
                var array = new JArray();
                NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(html);

                var es = doc.Select(step.ProcessStr);

                foreach (var e in es)
                {
                    var item = new JObject();
                    ExecuteStep(e.OuterHtml(), step.NextProcessStep, ref item);
                    array.Add(item);
                }

                data["array"] = array;
            }
            else if (step.Type == PageProcessStepTypes.RegExp)
            {
                Regex regex = new Regex(step.ProcessStr);
                var m = regex.Matches(html)[0];
                foreach (var n in step.DataNames.Split(';'))
                {
                    data[n] = m.Groups[n].Value;
                }
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
            var urls = _websiteProxy.NovelCrawlUrlList(deal.Guid);
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
