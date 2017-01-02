using D.Spider.Core.Events;
using D.Util.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// Spider 接口
    /// 内部组件通过 event 交互
    /// 内部维护一个依赖注入的容器
    /// </summary>
    public interface ISpider
        : IEventHandler<UrlCrawledEvent>
    {
        /// <summary>
        /// IOc Unity 的容器
        /// </summary>
        IUnityContainer UnityContainer { get; }

        /// <summary>
        /// Url 管理器
        /// </summary>
        IUrlManager UrlManager { get; }

        /// <summary>
        /// 设置 spider 内部使用的 unity 框架的配置文件路径
        /// TODO 暂时只能想到这样实现
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ISpider UnityConfigerPath(string path);

        /// <summary>
        /// 初始化整个 spider
        /// </summary>
        /// <returns></returns>
        ISpider Initialization();

        /// <summary>
        /// 启动 Spider 开始爬取网页内容
        /// </summary>
        /// <returns>用于链式编程</returns>
        ISpider Run();
    }
}
