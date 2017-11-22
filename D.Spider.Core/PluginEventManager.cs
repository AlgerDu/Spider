using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Interface.Plugin;
using D.Util.Interface;
using System.Collections.Concurrent;

namespace D.Spider.Core
{
    /// <summary>
    /// IPluginEventManager，IEventSender 的实现
    /// </summary>
    public class PluginEventManager : IPluginEventManager, IEventSender
    {
        ILogger _logger;

        /// <summary>
        /// 待分发的事件
        /// </summary>
        ConcurrentQueue<IPluginEvent> _waitingDistributeEvents;

        public PluginEventManager(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PluginEventManager>();
        }

        #region IEventSender 实现
        public bool Cancel(IPluginEvent e)
        {
            throw new NotImplementedException();
        }

        public void Send(IPluginEvent e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
