using D.Spider.Core;
using D.Spider.Core.Interface;
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
                .UnityConfigerPath(@"Unity.config")
                .Initialization()
                .Run();

            var manager = spider.UrlManager;

            string url = string.Empty;

            while ((url = System.Console.ReadLine()) != "q")
            {
                manager.AddUrl(new Url(url));
            }
        }
    }

    class ConsolePageProcess : IPageProcess
    {
        public void Process(IPage page)
        {
            System.Console.WriteLine(page.HtmlTxt);
        }
    }
}
