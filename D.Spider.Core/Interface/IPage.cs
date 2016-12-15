using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 下载好的页面
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Page 属于的 url
        /// </summary>
        IUrl Url { get; }

        /// <summary>
        /// 整个页面的 Html 文本
        /// </summary>
        string HtmlTxt { get; }
    }
}
