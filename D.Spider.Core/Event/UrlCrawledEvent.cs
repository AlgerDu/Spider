using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    public class UrlCrawledEvent : BaseEvent, IUrlCrawledEvent
    {
        public IPage Page { get; set; }
    }
}
