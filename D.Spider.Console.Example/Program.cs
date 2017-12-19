using D.Spider.Core;
using System;

namespace D.Spider.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = new SpiderBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();

            Console.ReadKey();

            spider.Stop();
        }
    }
}
