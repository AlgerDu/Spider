using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class Page : IPage
    {
        IUrl _url;
        string _htmlTxt;

        public Page(IUrl url, string html)
        {
            _url = url;
            _htmlTxt = html;
        }

        public IUrl Url
        {
            get
            {
                return _url;
            }
        }

        public string HtmlTxt
        {
            get
            {
                return _htmlTxt;
            }
        }
    }
}
