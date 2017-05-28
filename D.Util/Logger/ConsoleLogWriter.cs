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
        public ConsoleLogWriter()
        {
        }

        public void Write(ILogContext context)
        {
            lock (this)
            {
                Print("[");
                Print(context.Type.ToString(), LogContextTypeColor(context.Type));
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

                Print(context.Text.Replace("\r\n", "\r\n      "), LogContextTypeColor(context.Type));

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
