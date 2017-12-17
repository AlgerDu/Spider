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
    {
        /// <summary>
        /// 启动 Spider 开始爬取网页内容
        /// </summary>
        /// <returns>用于链式编程</returns>
        ISpider Run();

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        ISpider Stop();
    }
}
