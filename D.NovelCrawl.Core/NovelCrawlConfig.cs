using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬去的配置文件
    /// </summary>
    internal class NovelCrawlConfig : IConfigItem
    {
        public string Path
        {
            get
            {
                return "core.novelCrawl";
            }
        }

        /// <summary>
        /// 小说爬去的服务器地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 小说爬去核心的日志记录级别
        /// </summary>
        public string LogLevel { get; set; }

        public NovelCrawlConfig()
        {
            Server = "www.duzhiwei.online";
            LogLevel = Util.Interface.LogLevel.trce.ToString();
        }
    }
}
