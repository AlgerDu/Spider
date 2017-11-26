using D.Spider.Core.Interface.Plugin;
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
        IPlugin _plugin;
        Queue<PluginEventTask> _events;
        bool _isDeallingEvents;

        public Guid PluginInstaceUid { get => (Guid)_plugin.Symbol.InstanceUid; }

        public PluginEventQueue(
            IPlugin plugin)
        {
            _plugin = plugin;
            _isDeallingEvents = false;

            _events = new Queue<PluginEventTask>();
        }

        public void Enqueue(PluginEventTask e)
        {
            lock (_events)
            {

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
    }
}
