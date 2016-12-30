using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Models
{
    public class jQueryErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 返回错误码
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public jQueryErrorEventArgs(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
