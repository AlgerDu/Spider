using Autofac;
using D.Spider.Core.Interface;
using D.Util.Config;
using D.Util.Interface;
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

            var configCollector = CreateConfigCollector(startup);
            var pluginCollector = CreateIPluginCollecter(startup);

            var container = CreateAutofacContainer(startup, configCollector, pluginCollector);
            
            return container.Resolve<ISpider>();
        }

        public ISpiderBuilder UseStartup<T>() where T : IStartup, new()
        {
            _startupType = typeof(T);

            return this;
        }

        /// <summary>
        /// 创建一个 ConfigCollector，并且调用 Startup 
        /// </summary>
        /// <param name="startup"></param>
        /// <returns></returns>
        private IConfigCollector CreateConfigCollector(IStartup startup)
        {
            var configCollector = new ConfigCollector();

            startup.CollectConfig(configCollector);

            return configCollector;
        }

        /// <summary>
        /// 创建 autofac 的 container
        /// </summary>
        /// <param name="startup"></param>
        /// <param name="configCollector"></param>
        /// <returns></returns>
        private IContainer CreateAutofacContainer(IStartup startup, IConfigCollector configCollector, IPluginCollecter pluginCollecter)
        {
            //TODO：需要迁移到其它地方，暂时写在这里

            var builder = new ContainerBuilder();
            
            builder.AddUtils();
            builder.AddConfigProvider(configCollector.CreateProvider());

            //TODO：需要迁移到其它地方，暂时写在这里
            builder.RegisterInstance<IPluginCollecter>(pluginCollecter);

            startup.ConfigService(builder);

            return builder.Build();
        }

        /// <summary>
        /// 创建一个 IPluginCollecter
        /// </summary>
        /// <returns></returns>
        private IPluginCollecter CreateIPluginCollecter(IStartup startup)
        {
            var pluginCollecter = new PluginCollecter();
            startup.ManualCollectPlugin(pluginCollecter);

            return pluginCollecter;
        }
    }
}
