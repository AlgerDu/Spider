using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// log4net 配置
    /// </summary>
    internal class Log4netConfig : IConfigItem
    {
        public string Path
        {
            get
            {
                return "logging.log4net";
            }
        }

        /// <summary>
        /// 日志记录级别
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        public string OutFilePath { get; set; }

        /// <summary>
        /// 输出日志文件最大的大小
        /// </summary>
        public string MaxFileSize { get; set; }
    }
}
