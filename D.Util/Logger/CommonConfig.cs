using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    internal class LogFilter
    {
        public string Namesapce { get; set; }

        public LogLevel LogLevel { get; set; }
    }

    /// <summary>
    /// 通用的日志配置
    /// </summary>
    internal class CommonConfig : IConfigItem
    {
        public string Path
        {
            get
            {
                return "logging";
            }
        }

        public LogFilter[] Filters { get; set; }

        public CommonConfig()
        {
            Filters = new LogFilter[0];
        }
    }
}
