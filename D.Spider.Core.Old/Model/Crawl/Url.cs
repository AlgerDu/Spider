using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.Spider.Core.Model.Crawl
{
    /// <summary>
    /// IUrl 的实现
    /// </summary>
    public class Url : IUrl
    {
        string _relativePath;
        string _host;
        string _urlString;

        private Url()
        {
        }

        public Url(string url) : this()
        {
            _urlString = url;

            var array = Regex.Split(url, @"(^http[s]?://[^/:]+(:\d*)?)");

            if (array.Length == 2)
            {
                _host = array[1];
                _relativePath = "/";
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

        public Url(string host, string relativePath) : this(host + relativePath)
        {
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

        public string RelativePath
        {
            get
            {
                return _relativePath;
            }
        }

        #endregion

        #region IUrl 方法
        public bool Equals(IUrl r)
        {
            return this.ToString() == r.ToString();
        }

        /// <summary>
        /// 通过页面下的 href 创建完整的 url
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public IUrl CreateCompleteUrl(string href)
        {
            //href 可能的类型
            //1、完整的 url          eg: http://www.google.com
            //2、缺少 http: 的 url   eg: //read.qidian.com
            //3、                   eg: /1_1439/482060.html
            //4、                   eg: 5367499.html

            if (IsUrl(href))
            {
                return new Url(href);
            }
            else if (Regex.IsMatch(href, @"^//"))
            {
                return new Url("http:" + href);
            }
            else if (Regex.IsMatch(href, @"^/"))
            {
                return new Url(_host, href);
            }
            else
            {
                return new Url(this.ToString() + href);
            }
        }
        #endregion

        public override string ToString()
        {
            return _host + _relativePath;
        }

        /// <summary>
        /// 判断一个字符串是否为url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsUrl(string str)
        {
            try
            {
                string Url = @"^http[s]?://[^/:]+(:\d*)?";
                return Regex.IsMatch(str, Url);
            }
            catch
            {
                return false;
            }
        }
    }
}
