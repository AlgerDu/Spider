using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 普通任务和紧急任务的参数
    /// </summary>
    public interface IUrlCrawlOptions
    {
        /// <summary>
        /// 页面加载完成可能需要的时间；
        /// 给模拟浏览器执行 ajax 异步请求的时间，防止出现页面主体加载完成，但是 ajax 异步数据并没有加载完成；
        /// 此参数可能受网速、设备等条件影响
        /// </summary>
        TimeSpan PageLoadingTime { get; }
    }

    /// <summary>
    /// 循环任务的参数
    /// </summary>
    public interface ICycleCrawlOptions : IUrlCrawlOptions
    {
        /// <summary>
        /// 循环任务的事件间隔
        /// </summary>
        TimeSpan Interval { get; }

        /// <summary>
        /// 循环次数（-1 时为无限循环）
        /// </summary>
        int CycleCount { get; }
    }
}
