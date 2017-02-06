using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Util.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace D.Util.Web
{
    /// <summary>
    /// 使用 HttpWebRequest 和 HttpWebResponse 实现的 jQuer ajax 请求
    /// </summary>
    public class jQuery : IjQuery
    {
        const int _defaultTimeout = 10000;

        ILogger _logger;

        public jQuery(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<jQuery>();
        }

        public Task Ajax<T>(AjaxRequestTypes type, string url, object data, EventHandler<jQuerySuccessEventArgs<T>> success, EventHandler<jQueryErrorEventArgs> error = null, int timeout = -1) where T : class
        {
            return Task.Run(() =>
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = type.ToString();
                request.Timeout = timeout == -1 ? _defaultTimeout : timeout;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

                try
                {
                    if (type == AjaxRequestTypes.POST && data != null)
                    {
                        var json = JsonConvert.SerializeObject(data);

                        request.ContentType = "application/json";

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Stream respStream = response.GetResponseStream();
                            using (StreamReader reader = new StreamReader(
                                respStream,
                                Encoding.GetEncoding(response.CharacterSet)
                                ))
                            {
                                var txt = reader.ReadToEnd();
                                var rData = TxtToObject<T>(txt);
                                success?.Invoke(this, new jQuerySuccessEventArgs<T>(rData as T));
                            }
                        }
                        else
                        {
                            error?.Invoke(this, new jQueryErrorEventArgs(response.StatusCode));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(WebException))
                    {
                        var response = (ex as WebException).Response as HttpWebResponse;

                        if (response != null)
                        {
                            error?.Invoke(this, new jQueryErrorEventArgs(response.StatusCode));
                        }
                        else
                        {
                            _logger.LogWarning("jQuery 执行 ajax 请求时发生错误 WebException：" + ex.ToString());
                        }
                    }
                    else
                    {
                        _logger.LogWarning("jQuery 执行 ajax 请求时发生错误：" + ex.ToString());
                    }
                }
            });
        }

        public Task Get<T>(string url, EventHandler<jQuerySuccessEventArgs<T>> success) where T : class
        {
            return Ajax(AjaxRequestTypes.GET, url, null, success);
        }

        public Task Post<T>(string url, object data, EventHandler<jQuerySuccessEventArgs<T>> success) where T : class
        {
            return Ajax(AjaxRequestTypes.POST, url, data, success);
        }

        private object TxtToObject<T>(string txt) where T : class
        {
            if (typeof(T) == typeof(string))
            {
                return txt;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(txt);
            }
        }
    }
}
