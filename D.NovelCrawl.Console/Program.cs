using D.NovelCrawl.Core.Interface;
using D.Spider.Core;
using Microsoft.Practices.Unity;
using System;

namespace D.NovelCrawl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = new DSpider()
                .UnityConfigerPath(AppDomain.CurrentDomain.BaseDirectory + @"Unity.config")
                .Initialization()
                .Run();

            var novelCrawl = spider.UnityContainer.Resolve<INvoelCrawl>();

            novelCrawl.Run();

            System.Console.ReadKey();
        }
    }
}
