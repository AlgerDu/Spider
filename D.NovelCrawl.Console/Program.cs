using D.NovelCrawl.Core;
using D.Spider.Core;
using D.Util.Logger.Console;
using D.Util.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            System.Console.ReadKey();
        }
    }
}
