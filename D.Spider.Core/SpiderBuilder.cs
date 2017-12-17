using Autofac;
using D.Spider.Core.Interface;
using D.Util.Config;
using D.Utils.AutofacExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class SpiderBuilder : ISpiderBuilder
    {
        Type _startupType;

        public SpiderBuilder(string[] args)
        {
            //TODO 处理一些运行参数
        }

        public ISpider Build()
        {
            var startup = _startupType.Assembly.CreateInstance(_startupType.FullName) as IStartup;

            var configCollector = new ConfigCollector();

            startup.CollectConfig(configCollector);

            var builder = new ContainerBuilder();

            builder.AddUtils();
            builder.AddConsoleLogWriter();
            builder.AddConfigProvider(configCollector.CreateProvider());

            startup.ConfigService(builder);

            var container = builder.Build();

            return container.Resolve<ISpider>();
        }

        public ISpiderBuilder UseStartup<T>() where T : IStartup, new()
        {
            _startupType = typeof(T);

            return this;
        }
    }
}
