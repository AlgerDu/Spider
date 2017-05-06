using D.Util.Interface;
using log4net.Appender;
using log4net.Repository.Hierarchy;
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
        string _guid;

        log4net.ILog _log;

        Log4netConfig _config;

        /// <summary>
        /// TODO 暂时还没有想到如何转入配置参数
        /// </summary>
        public Log4netWriter(
            IConfig config
            )
        {
            _config = config.GetItem<Log4netConfig>();

            _guid = Guid.NewGuid().ToString();

            //过滤器
            log4net.Filter.LevelRangeFilter levfilter = new log4net.Filter.LevelRangeFilter();
            levfilter.LevelMax = log4net.Core.Level.Fatal;
            levfilter.LevelMin = log4net.Core.Level.Info;
            levfilter.ActivateOptions();

            //格式化 一定要，不然没有日志输出
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout("%message");
            layout.Header = "------ 邪恶的分割线 开始 ------" + Environment.NewLine;
            layout.Footer = "------ 邪恶的分割线 结束 ------" + Environment.NewLine;

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.MaxSizeRollBackups = 100;
            roller.MaximumFileSize = _config.MaxFileSize;
            roller.StaticLogFileName = true;
            roller.File = _config.OutFilePath;
            roller.Name = _guid;
            roller.Encoding = Encoding.Unicode;

            roller.AddFilter(levfilter);
            roller.Layout = layout;

            //初始化，不然没有用处，也不会报错，不过貌似不同的 log4net 版本初始化的方式不同，在网上看到不同的使用方式
            roller.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(roller);


            _log = log4net.LogManager.GetLogger(_guid);
        }

        public void Write(ILogContext context)
        {
            //按照 log4net 的命名方式
            string message = string.Empty;

            message += string.Format("[{0}][{1}][thread {2}]",
                context.Type.ToString(),
                context.CreateTime.ToString("HH:mm:ss ffff"),
                context.ThreadID)
                + Environment.NewLine;

            message += string.Format("      {0}", context.ClassFullName)
                + Environment.NewLine;

            message += "      " + context.Text.Replace("\r\n", "\r\n      ")
                + Environment.NewLine;

            _log.Info(message);
        }
    }
}
