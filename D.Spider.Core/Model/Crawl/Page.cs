using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Model.Crawl
{
    public class Page : IPage
    {
        public IUrl Url { get; set; }

        public string HtmlTxt { get; set; }
    }
}
