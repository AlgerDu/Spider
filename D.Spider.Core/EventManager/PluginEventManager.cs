using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Util.Interface;
using System.Collections.Concurrent;
using D.Spider.Core.EventManager;

namespace D.Spider.Core
{
    /// <summary>
    /// IPluginEventManager，IEventSender 的实现
    /// </summary>
    public class PluginEventManager : IPluginEventManager, IEventBus
    {
        ILogger _logger;

        bool _isRunning;

        Dictionary<int, HandlerShell> dic_Plugins;

        Dictionary<Guid, PluginEventTask> dic_EventTasks;

        public PluginEventManager(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<PluginEventManager>();

            dic_EventTasks = new Dictionary<Guid, PluginEventTask>();
            dic_Plugins = new Dictionary<int, HandlerShell>();
        }

        public bool IsRunning => _isRunning;

        #region IEventBus 实现
        public int Cancel<eType>(eType e) where eType : IPluginEvent
        {
            throw new NotImplementedException();
        }

        public void Publish<eType>(eType e) where eType : IPluginEvent
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPluginEventManager 实现
        public IPluginEventManager Run()
        {
            _isRunning = true;

            return this;
        }

        public IPluginEventManager Stop()
        {
            _isRunning = false;

            return this;
        }

        public void AddPlugin(IPlugin plugin)
        {
            var shell = new HandlerShell(plugin);

            lock (dic_Plugins)
            {
                if (dic_Plugins.ContainsKey(shell.Key))
                {
                    var containedShell = dic_Plugins[shell.Key];
                    _logger.LogWarning(
                        $"{shell.Plugin} 和 {containedShell.Plugin} 具有相同的 {shell.Key}\n" +
                        $"不能在 event manager 中添加 {shell.Plugin}");
                }
                else
                {
                    dic_Plugins.Add(shell.Key, shell);
                }
            }
        }

        public void RemovePlugin(IPlugin plugin)
        {
            if (plugin == null)
            {
                _logger.LogDebug($"RemovePlugin 传入的 plugin 为 null");
            }
            else
            {
                var key = plugin.Symbol.InstanceID.Value;

                lock (dic_Plugins)
                {
                    if (dic_Plugins.ContainsKey(key))
                    {
                        dic_Plugins.Remove(key);
                    }
                    else
                    {
                        _logger.LogWarning($"event manager 没有对 {plugin} 的处理");
                    }
                }
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
                ////var toPlugins = _pluginManager.Search(task.PluginEvent.ToPluginSymbol);

                //if (toPlugins.Count() == 0)
                //{

                //}
                //else
                //{
                //    var e = task.PluginEvent;
                //    _logger.LogInformation($"{e.FromPlugin} 发送的事件 {e} 没有找到任何");
                //}
            });
        }
    }
}
