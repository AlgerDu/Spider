using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    public class PageDownloadCompleteEvent : BaseEvent, IPageDownloadCompleteEvent
    {
        public IPage Page { get; set; }

        public Guid PageDownloadEventUid { get; set; }
    }
}
