using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 页面下载事件，由 downloader 处理
    /// </summary>
    public interface IPageDownloadEvent : IPluginEvent
    {
        /// <summary>
        /// 需要下载页面的 url
        /// </summary>
        IUrl Url { get; }

        /// <summary>
        /// 下载 url 对应的页面时使用的一些参数
        /// </summary>
        IPageDownloadOptions DownloadOptions { get; }
    }
}
