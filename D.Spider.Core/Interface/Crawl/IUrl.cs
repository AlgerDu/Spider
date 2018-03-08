using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// Url 对象
    /// </summary>
    public interface IUrl : IEquatable<IUrl>
    {
        /// <summary>
        /// 指向此链接的 URl
        /// </summary>
        IList<IUrl> FromUrls { get; }

        /// <summary>
        /// 主机地址
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 相对路径
        /// </summary>
        string RelativePath { get; }

        /// <summary>
        /// 通过页面下的 href 创建完整的 url
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        IUrl CreateCompleteUrl(string href);
    }
}
