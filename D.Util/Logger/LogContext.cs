using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    public class LogContext : ILogContext
    {
        public string ClassFullName { get; set; }

        public DateTime CreateTime { get; set; }

        public int EventID { get; set; }

        public string Text { get; set; }

        public int ThreadID { get; set; }

        public LogLevel Type { get; set; }
    }
}
