using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Models
{
    public class jQuerySuccessEventArgs<T> : EventArgs
        where T : class
    {
        /// <summary>
        /// 请求成功返回的数据
        /// </summary>
        public T Data { get; private set; }

        public jQuerySuccessEventArgs(T data)
        {
            Data = data;
        }
    }
}
