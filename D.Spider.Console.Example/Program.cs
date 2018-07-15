using D.Spider.Core;
using D.Spider.Core.Interface;
using Microsoft.Extensions.Logging;
using System;

namespace D.Spider.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = new SpiderBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((loggerFactory, configer) =>
                {
                    loggerFactory.AddConsole(LogLevel.Trace);
                })
                .Build()
                .Run();

            Console.ReadKey();

            spider.Stop();
        }
    }
}
