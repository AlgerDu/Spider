using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger.NullLogger
{
    /// <summary>
    /// 空日志对象
    /// </summary>
    class NullLogger : ILogger
    {
        public void LogCritical(string text)
        {
        }

        public void LogDebug(string text)
        {
        }

        public void LogError(string text)
        {
        }

        public void LogInformation(string text)
        {
        }

        public void LogTrace(string text)
        {
        }

        public void LogWarning(string text)
        {
        }
    }
}
