using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Interface.Plugin;
using D.Util.Interface;
using System.Collections.Concurrent;
using D.Spider.Core.Model;

namespace D.Spider.Core
{
    /// <summary>
    /// IPluginEventManager，IEventSender 的实现
    /// </summary>
    public class PluginEventManager : IPluginEventManager, IEventSender
    {
        ILogger _logger;
        IPluginManager _pluginManager;

        /// <summary>
        /// 所以这在处理中的插件事件
        /// </summary>
        ConcurrentDictionary<Guid, PluginEventTask> _allEventTasks;

        /// <summary>
        /// 每个插件正在等待执行的插件
        /// </summary>
        Dictionary<Guid, ConcurrentQueue<IPluginEvent>> _perPluginWaitingExecutingEvents;

        public PluginEventManager(
            ILoggerFactory loggerFactory
            , IPluginManager pluginManager)
        {
            _logger = loggerFactory.CreateLogger<PluginEventManager>();
            _pluginManager = pluginManager;

            _allEventTasks = new ConcurrentDictionary<Guid, PluginEventTask>();
            _perPluginWaitingExecutingEvents = new Dictionary<Guid, ConcurrentQueue<IPluginEvent>>();
        }

        #region IEventSender 实现
        public bool Cancel(IPluginEvent e)
        {
            throw new NotImplementedException();
        }

        public void Send(IPluginEvent e)
        {
            _logger.LogInformation($"{e.FromPlugin} 发送事件 {e}");

            var task = new PluginEventTask(e);

            if (_allEventTasks.TryAdd(task.Uid, task))
            {
                DistributeEvents(task);
            }
            else
            {
                _logger.LogWarning($"向 allEventTasks 列表中添加事件 {e} 失败");
            }
        }
        #endregion

        /// <summary>
        /// 从待分配队列中取出事件，添加到需要处理这个事件的插件事件队列中
        /// </summary>
        /// <returns></returns>
        private Task DistributeEvents(PluginEventTask task)
        {
            return Task.Run(() =>
            {
            });
        }
    }
}
