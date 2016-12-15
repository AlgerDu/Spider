using D.Spider.Core;
using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            string url = string.Empty;

            while ((url = System.Console.ReadLine()) != "q")
            {
                if (Url.IsUrl(url))
                {
                    manager.AddUrl(new Url(url));
                }
                else
                {
                    System.Console.WriteLine(url + " 不是一个正确的 url");
                }
            }
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
            _logger.LogDebug("test dbug");
            _logger.LogError("test fail");
            _logger.LogCritical("test crit");
            _logger.LogTrace("test trac");
            _logger.LogWarning("test warn");
            //System.Console.WriteLine(page.HtmlTxt.Substring(0, 50)[0]);
        }
    }
}
