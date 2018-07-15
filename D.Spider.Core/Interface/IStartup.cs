using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 启动配置，学习 asp.net core 的 Startup 以及其它类似的设计
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// 全局加载的配置
        /// </summary>
        IConfiguration Configuration { get; set; }

        /// <summary>
        /// 依赖注入；
        /// 算了，还是使用 autofac 吧，哎，纠结
        /// </summary>
        /// <param name="services"></param>
        void ConfigService(ContainerBuilder services);

        /// <summary>
        /// 手动添加一些插件
        /// </summary>
        /// <param name="pluginCollecter"></param>
        void ManualCollectPlugin(IPluginCollecter pluginCollecter);
    }
}
