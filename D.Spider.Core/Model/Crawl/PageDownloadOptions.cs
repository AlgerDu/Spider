using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Model.Crawl
{
    public class PageDownloadOptions : IPageDownloadOptions
    {
        public TimeSpan PageLoadingTime { get; set; }

        public Dictionary<string, string> HttpHeaders => throw new NotImplementedException();
    }
}
