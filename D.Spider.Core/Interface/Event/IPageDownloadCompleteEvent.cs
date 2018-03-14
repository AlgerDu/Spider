using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 页面下载完成事件
    /// </summary>
    public interface IPageDownloadCompleteEvent : IPluginEvent
    {
        /// <summary>
        /// 是哪个下载事件的下载任务完成了
        /// </summary>
        Guid PageDownloadEventUid { get; }

        /// <summary>
        /// 下载好的页面
        /// </summary>
        IPage Page { get; }
    }
}
