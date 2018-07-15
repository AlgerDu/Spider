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
        /// 爬取任务的类型（默认是 Normal）
        /// </summary>
        UrlCrwalEventType CrawlType { get; }
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
