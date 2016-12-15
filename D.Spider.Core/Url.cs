using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// IUrl 的实现
    /// </summary>
    public class Url : IUrl
    {
        string _relativeUrl;

        public IList<IUrl> FromUrls
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Host
        {
            get
            {
                return "";
            }
        }

        public DateTime? LastCrawledTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RelativePath
        {
            get
            {
                return _relativeUrl;
            }
        }

        public bool Equal(IUrl r)
        {
            return Host == r.Host && RelativePath == r.RelativePath;
        }

        public bool NeedCrawl()
        {
            return true;
        }

        public Url(string url)
        {
            _relativeUrl = url;
        }
    }
}
