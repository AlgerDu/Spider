using D.Spider.Core.Event;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Extension
{
    public static class UrlEventExtensions
    {
        public static IUrlEvent CreateUrlEvent(this IEventFactory eventFactory, string url)
        {
            return null;
        }
    }
}
