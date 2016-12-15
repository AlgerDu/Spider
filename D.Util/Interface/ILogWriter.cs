using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 真实的日志记录者
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="context"></param>
        void Write(ILogContext context);
    }
}
