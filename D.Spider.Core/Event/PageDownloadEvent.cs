using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    internal class PageDownloadEvent : BaseEvent, IPageDownloadEvent
    {
        public IUrl Url { get; set; }

        public PageDownloadEvent():base()
        {
            DealType = DealPluginEventType.First;

            AddToPluginSymbol(new PluginSymbol
            {
                PType = PluginType.Downloader
            });
        }
    }
}
