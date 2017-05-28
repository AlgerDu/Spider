using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    internal class NullLogger : ILogger
    {
        public void LogCritical(string format, params object[] args)
        {
            
        }

        public void LogCritical(int eventID, string format, params object[] args)
        {
            
        }

        public void LogDebug(string format, params object[] args)
        {
            
        }

        public void LogDebug(int eventID, string format, params object[] args)
        {
            
        }

        public void LogError(string format, params object[] args)
        {
            
        }

        public void LogError(int eventID, string format, params object[] args)
        {
            
        }

        public void LogInformation(string format, params object[] args)
        {
            
        }

        public void LogInformation(int eventID, string format, params object[] args)
        {
            
        }

        public void LogTrace(string format, params object[] args)
        {
            
        }

        public void LogTrace(int eventID, string format, params object[] args)
        {
            
        }

        public void LogWarning(string format, params object[] args)
        {
            
        }

        public void LogWarning(int eventID, string format, params object[] args)
        {
            
        }
    }
}
