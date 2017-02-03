using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// 封装 log4net 到自定义的接口 ILogWriter
    /// </summary>
    public class Log4netWriter : ILogWriter
    {
        public Log4netWriter()
        {

        }

        public void Write(ILogContext context)
        {
            throw new NotImplementedException();
        }
    }
}
