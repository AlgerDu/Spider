using D.Spider.Core.Interface;
using D.Util;
using D.Util.Event;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Events
{
    /// <summary>
    /// IDownloader 下载完一个 Url 对应的页面之后发布此事件
    /// </summary>
    public class UrlCrawledEvent : BaseEvent, IEvent
    {
        IPage _page;

        public UrlCrawledEvent(IPage page)
        {
            _page = page;
        }

        public IPage Page
        {
            get
            {
                return _page;
            }
        }
    }
}
