using Autofac;
using D.Spider.Core.Extension;
using D.Spider.Core.Interface;
using D.Spider.Core.SpiderScriptEngine;
using D.Spider.Extension.Plugin;
using D.Util.Interface;
using D.Utils.AutofacExt;
using System;

namespace D.Spider.Example
{
    /// <summary>
    /// IStartup 样例
    /// </summary>
    class Startup : IStartup
    {
        public void CollectConfig(IConfigCollector configCollector)
        {
            Console.WriteLine("Startup 收集配置");
        }

        public void ConfigService(ContainerBuilder builder)
        {
            Console.WriteLine("Startup 配置服务（依赖注入）");

            builder.RegisterType<SsEngine>().As<ISpiderScriptEngine>().SingleInstance();

            builder.AddConsoleLogWriter();

            builder.AddSpiderCoreService();
        }

        public void ManualCollectPlugin(IPluginCollecter pluginCollecter)
        {
            Console.WriteLine("Startup 手动添加插件");

            pluginCollecter.Collect<CefDownloader>();

            pluginCollecter.Collect<MiniPlugin>();
        }
    }
}
