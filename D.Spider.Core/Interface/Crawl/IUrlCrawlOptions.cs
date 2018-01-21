using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 爬去 url 的一些参数，这个参数可能会在后面经常调整；
    /// 暂时将所有类型任务的参数都写在这里了，应该分几个接口会比较好
    /// </summary>
    public interface IUrlCrawlOptions
    {
        /// <summary>
        /// 这个算是一个举例吧
        /// </summary>
        string Token { get; }

        /// <summary>
        /// 开始时间，定时任务和循环任务的开始时间
        /// </summary>
        DateTime? StartTime { get; }

        /// <summary>
        /// 循环任务的事件间隔
        /// </summary>
        TimeSpan? Interval { get; }

        /// <summary>
        /// 循环次数（-1 时为无限循环）
        /// </summary>
        int? CycleCount { get; }
    }
}
