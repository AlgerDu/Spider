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
        string _htmlTxt;

        public Page(string html)
        {
            _htmlTxt = html;
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
