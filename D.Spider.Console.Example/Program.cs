using Autofac;
using D.Spider.Core;
using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Example
{
    /// <summary>
    /// IStartup 样例
    /// </summary>
    class Startup : IStartup
    {
        public Startup()
        {

        }

        public void CollectConfig(IConfigCollector configCollector)
        {

        }

        public void ConfigService(ContainerBuilder builder)
        {
            throw new NotImplementedException();
        }

        public void ManualCollectPlugin(IPluginCollecter pluginCollecter)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new SpiderBuilder()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
