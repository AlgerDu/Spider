using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// 空日志 writer
    /// </summary>
    public class NullLogWriter : ILogWriter
    {
        public void Write(ILogContext context)
        {
            //什么也不做
        }
    }
}
