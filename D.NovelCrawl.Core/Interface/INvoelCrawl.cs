using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Interface
{
    /// <summary>
    /// 小说爬取接口
    /// 继承 IPageProcess 处理爬取好的网页
    /// </summary>
    public interface INvoelCrawl : IPageProcess
    {
        /// <summary>
        /// 开始运行小说爬取程序
        /// </summary>
        /// <returns></returns>
        INvoelCrawl Run();
    }
}
