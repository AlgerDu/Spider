using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// 单个插件的待执行事件队列
    /// </summary>
    internal class PluginEventQueue
    {
        ILogger _logger;
        IPlugin _plugin;
        Queue<PluginEventTask> _events;
        bool _isDeallingEvents;

        public Guid PluginInstaceUid { get => (Guid)_plugin.Symbol.InstanceUid; }

        public PluginEventQueue(
            IPlugin plugin
            , ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<PluginEventQueue>();
            _plugin = plugin;
            _isDeallingEvents = false;

            _events = new Queue<PluginEventTask>();
        }

        public void Enqueue(PluginEventTask e)
        {
            lock (_events)
            {
                _events.Enqueue(e);
            }
        }

        /// <summary>
        /// 当一个插件停止，需要将其全部待执行的事件清理
        /// </summary>
        public void Clean()
        {
            lock (this)
            {
                foreach (var task in _events)
                {
                    task.CancelCount++;
                }

                _events.Clear();
            }
        }

        /// <summary>
        /// 如果当前没有线程在处理任务，则开始处理任务
        /// </summary>
        /// <returns></returns>
        private Task RunEventTask()
        {
            return Task.Run(() =>
            {
                lock (this)
                {
                    if (_isDeallingEvents)
                    {
                        return;
                    }

                    _isDeallingEvents = true;
                }

                while (_events.Count > 0)
                {
                    try
                    {
                        var task = _events.Dequeue();

                        DealEvent(task);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{_plugin} 执行事件发生错误：{ex.StackTrace}");
                        break;
                    }
                }

                lock (this)
                {
                    _isDeallingEvents = false;
                }
            });
        }

        private void DealEvent(PluginEventTask task)
        {
            if (_plugin.State == PluginState.Running)
            {
                var e = task.PluginEvent;
                if (e.State == PluginEventState.Terminate)
                {
                    var eType = e.GetType();
                    var handlerType = _plugin.GetType().GetInterface(typeof(IPluginEventHandler<>).Name);

                    var has = handlerType.GetGenericArguments().FirstOrDefault(tt => tt == eType);

                    handlerType.InvokeMember("Handle", System.Reflection.BindingFlags.Public, null, _plugin, new object[] { e });
                }
                else
                {
                    task.CancelCount++;
                }
            }
            else
            {
                task.CancelCount++;
            }
        }
    }
}
