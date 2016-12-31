using D.Spider.Core;
using D.Spider.Core.Interface;
using D.Util.Interface;
using D.Util.Models;
using D.Util.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.Spider.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = new DSpider()
                .UnityConfigerPath(AppDomain.CurrentDomain.BaseDirectory + @"Unity.config")
                .Initialization()
                .Run();

            var manager = spider.UrlManager;

            manager.AddUrl("http://book.qidian.com/info/36026917#Catalog");

            System.Console.ReadKey();
        }
    }

    class ConsolePageProcess : IPageProcess
    {
        ILogger _logger;

        public ConsolePageProcess(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsolePageProcess>();
        }

        public void Process(IPage page)
        {
            _logger.LogInformation("解析 html");
            //_logger.LogInformation(page.HtmlTxt);
            ////Regex regex = new Regex("<!-- start 目录分卷 -->[\\s\\S]*<!-- end 目录分卷 -->");
            ////var mc = regex.Matches(page.HtmlTxt);
            ////var html = mc[0].Value;
            ////regex = new Regex("<a[^>]*>(?<name>[\\s\\S]*?)</a>");
            ////mc = regex.Matches(html);
            ////foreach (Match m in mc)
            ////{
            ////    _logger.LogInformation(m.Groups["name"].Value);
            ////}

            //NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(page.HtmlTxt);
            ////var e = doc.GetElementsByAttribute("data-rid");
            //var es = doc.Select("div.volume li a");

            //foreach (var e in es)
            //{
            //    _logger.LogInformation(e.OuterHtml());
            //}
        }
    }
}
