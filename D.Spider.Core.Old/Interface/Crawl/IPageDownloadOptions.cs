using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// downloader 下载页面的过程中使用的一些参数
    /// </summary>
    public interface IPageDownloadOptions
    {
        /// <summary>
        /// 页面加载完成可能需要的时间；
        /// 给模拟浏览器执行 ajax 异步请求的时间，防止出现页面主体加载完成，但是 ajax 异步数据并没有加载完成；
        /// 此参数可能受网速、设备等条件影响
        /// </summary>
        TimeSpan PageLoadingTime { get; }

        /// <summary>
        /// 预留，发送 http 请求时需要填充的 request header 字段
        /// </summary>
        Dictionary<string, string> HttpHeaders { get; }
    }
}
