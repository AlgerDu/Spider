using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISpiderBuilder
    {
        /// <summary>
        /// 使用 Startup 类
        /// </summary>
        /// <typeparam name="T">自定义 ISpiderBuilder</typeparam>
        /// <returns></returns>
        ISpiderBuilder UseStartup<T>() where T : IStartup, new();

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="configAction"></param>
        /// <returns></returns>
        ISpiderBuilder ConfigureAppConfiguration(Action<IConfigurationBuilder> configAction);

        /// <summary>
        /// 加载日志配置
        /// </summary>
        /// <param name="configureLoggingAction"></param>
        /// <returns></returns>
        ISpiderBuilder ConfigureLogging(Action<ILoggerFactory, IConfiguration> configureLoggingAction);

        /// <summary>
        /// 创建一个 Spider
        /// </summary>
        /// <returns></returns>
        ISpider Build();
    }
}
