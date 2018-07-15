using Autofac;
using D.Util.Interface;
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
        /// 手动添加一些配置或者设置配置文件等等
        /// </summary>
        /// <param name="configCollector"></param>
        void CollectConfig(IConfigCollector configCollector);

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="builder"></param>
        void ConfigService(ContainerBuilder builder);

        /// <summary>
        /// 手动添加一些插件
        /// </summary>
        /// <param name="pluginCollecter"></param>
        void ManualCollectPlugin(IPluginCollecter pluginCollecter);
    }
}
