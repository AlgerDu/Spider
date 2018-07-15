using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 当一个 url 页面下载完成之后，url manager 发布此事件给请求下载此 url 的业务处理插件
    /// </summary>
    public interface IUrlCrawledEvent : IPluginEvent
    {
        /// <summary>
        /// 下载下来的页面数据
        /// </summary>
        IPage Page { get; }
    }
}
