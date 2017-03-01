using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 处理爬取好的页面
    /// 可以实现自定义的页面处理器
    /// </summary>
    public interface IPageProcess
    {
        /// <summary>
        /// 处理爬取好的页面
        /// </summary>
        /// <param name="url"></param>
        void Process(IUrl url);
    }
}
