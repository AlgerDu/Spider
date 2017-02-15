using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;

namespace D.Spider
{
    /// <summary>
    /// 对 cefsharp 的封装用来爬取动态网页
    /// </summary>
    public class CefDownloader : IDownloader
    {
        /// <summary>
        /// 处理 urlManager 发布的 UrlWaitingEvent
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void Handle(UrlWaitingEvent e)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
