using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// 控制台日志 writer
    /// </summary>
    public class ConsoleLogWriter : ILogWriter
    {
        /// <summary>
        /// console 日志配置文件
        /// </summary>
        class Config : IConfigItem
        {
            public string Path
            {
                get
                {
                    return "logging.writer.console";
                }
            }

            /// <summary>
            /// 日志记录级别
            /// </summary>
            public string LogLevel { get; set; }

            public LogLevel Level
            {
                get
                {
                    try
                    {
                        return (LogLevel)Enum.Parse(typeof(LogLevel), LogLevel);
                    }
                    catch
                    {
                        return Util.Interface.LogLevel.info;
                    }
                }
            }
        }

        LogLevel _level;

        public ConsoleLogWriter(
            IConfig config
            )
        {
            _level = config.GetItem<Config>().Level;
        }

        public void Write(ILogContext context)
        {
            if (context.Level < _level)
            {
                return;
            }

            lock (this)
            {
                Print("[");
                Print(context.Level.ToString(), LogContextTypeColor(context.Level));
                Print("]");

                Print("[");
                Print(context.CreateTime.ToString("HH:mm:ss ffff"));
                Print("]");

                Print("[");
                Print("thread " + context.ThreadID);
                Print("]");

                Print("\r\n      ");

                Print(context.ClassFullName, ConsoleColor.White);

                Print("\r\n      ");

                Print(context.Text.Replace("\r\n", "\r\n      "), LogContextTypeColor(context.Level));

                Print("\r\n");
            }
        }

        void Print(string txt, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(txt);
        }

        /// <summary>
        /// 根据不同的日志类型输出不同的颜色
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ConsoleColor LogContextTypeColor(LogLevel type)
        {
            switch (type)
            {
                case LogLevel.trce: return ConsoleColor.Gray;
                case LogLevel.dbug: return ConsoleColor.Blue;
                case LogLevel.info: return ConsoleColor.Green;
                case LogLevel.warn: return ConsoleColor.Yellow;
                case LogLevel.fail: return ConsoleColor.Red;
                case LogLevel.crit: return ConsoleColor.Red;
                default: return ConsoleColor.Black;
            }
        }
    }
}
