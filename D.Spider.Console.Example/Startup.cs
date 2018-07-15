using Autofac;
using D.Spider.Core.Extension;
using D.Spider.Core.Interface;
using D.Spider.Core.SpiderScriptEngine;
using D.Spider.Extension.Plugin;
using Microsoft.Extensions.Configuration;
using System;

namespace D.Spider.Example
{
    /// <summary>
    /// IStartup 样例
    /// </summary>
    class Startup : IStartup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigService(ContainerBuilder builder)
        {
            Console.WriteLine("Startup 配置服务（依赖注入）");

            builder.RegisterType<SsEngine>().As<ISpiderScriptEngine>().SingleInstance();
            
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
