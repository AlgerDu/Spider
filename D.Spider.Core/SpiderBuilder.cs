using Autofac;
using Autofac.Extensions.DependencyInjection;
using D.Spider.Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class SpiderBuilder : ISpiderBuilder
    {
        IStartup _startup;

        Action<IConfigurationBuilder> _configAction;
        Action<ILoggerFactory, IConfiguration> _configureLoggingAction;

        public SpiderBuilder(string[] args)
        {
            //TODO 处理一些运行参数
        }

        public ISpiderBuilder UseStartup<T>() where T : IStartup, new()
        {
            _startup = new T();

            return this;
        }

        public ISpiderBuilder ConfigureAppConfiguration(Action<IConfigurationBuilder> configAction)
        {
            _configAction = configAction;
            return this;
        }

        public ISpiderBuilder ConfigureLogging(Action<ILoggerFactory, IConfiguration> configureLoggingAction)
        {
            _configureLoggingAction = configureLoggingAction;
            return this;
        }

        public ISpider Build()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var loggerFactory = new LoggerFactory();

            _configAction(configurationBuilder);

            _startup.Configuration = configurationBuilder.Build();

            _configureLoggingAction(loggerFactory, _startup.Configuration);

            var builder = new ContainerBuilder();

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory>(loggerFactory);

            _startup.ConfigService(builder);

            builder.Populate(services);

            //TODO：需要迁移到其它地方，暂时写在这里
            builder.RegisterInstance<IPluginCollecter>(CreatePluginCollecter(_startup));

            var container = builder.Build();

            return container.Resolve<ISpider>();
        }

        /// <summary>
        /// 创建一个 IPluginCollecter
        /// </summary>
        /// <returns></returns>
        private IPluginCollecter CreatePluginCollecter(IStartup startup)
        {
            var pluginCollecter = new PluginCollecter();
            startup.ManualCollectPlugin(pluginCollecter);

            return pluginCollecter;
        }
    }
}
