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
    public interface IUrl
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
        /// 最后一次爬取的时间
        /// </summary>
        DateTime? LastCrawledTime { get; set; }

        /// <summary>
        /// Url 爬取的时间间隔
        /// 时间单位 秒
        /// </summary>
        int Interval { get; set; }

        /// <summary>
        /// 获取完整的 Url 字符串
        /// </summary>
        string String { get; }

        /// <summary>
        /// 判断两个 url 是否时同一个 url
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        bool Equal(IUrl r);

        /// <summary>
        /// URL 是否需要爬取
        /// </summary>
        /// <returns></returns>
        bool NeedCrawl();
    }
}
