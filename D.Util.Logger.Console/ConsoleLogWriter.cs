using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger.Console
{
    class ConsoleLogWriter : ILogWriter
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
            System.Console.ForegroundColor = color;
            System.Console.Write(txt);
        }

        ConsoleColor LogContextTypeColor(LogContextType type)
        {
            switch (type)
            {
                case LogContextType.trce: return ConsoleColor.Gray;
                case LogContextType.dbug: return ConsoleColor.Blue;
                case LogContextType.info: return ConsoleColor.Green;
                case LogContextType.warn: return ConsoleColor.Yellow;
                case LogContextType.fail: return ConsoleColor.Red;
                case LogContextType.crit: return ConsoleColor.Red;
                default: return ConsoleColor.Black;
            }
        }
    }
}
