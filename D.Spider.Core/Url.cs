using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// IUrl 的实现
    /// </summary>
    public class Url : IUrl
    {
        string _relativePath;
        string _host;
        string _urlString;

        public Url(string url)
        {
            _urlString = url;

            var array = Regex.Split(url, @"(^http[s]?://[^/:]+(:\d*)?)");

            if (array.Length == 2)
            {
                _host = array[1];
                _relativePath = "";
            }
            else if (array.Length == 3)
            {
                _host = array[1];
                _relativePath = array[2];
            }
            else
            {
                throw new Exception("使用正则表达式获取 url 中的主机地址时失败");
            }
        }

        public Url(string host, string relativePath)
        {
            _host = host;
            _relativePath = relativePath;

            _urlString = _host + _relativePath;
        }

        #region IRul 属性
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
                return _host;
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
                return _relativePath;
            }
        }

        public string UrlString
        {
            get
            {
                return _urlString;
            }
        }
        #endregion

        #region IUrl 方法
        public bool Equal(IUrl r)
        {
            return UrlString == r.UrlString;
        }

        public bool NeedCrawl()
        {
            return true;
        }
        #endregion

        /// <summary>
        /// 判断一个字符串是否为url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(string str)
        {
            try
            {
                string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                return Regex.IsMatch(str, Url);
            }
            catch
            {
                return false;
            }
        }
    }
}
