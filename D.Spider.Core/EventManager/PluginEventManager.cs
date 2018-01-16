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

        public bool IsRunning => _isRunning;

        public PluginEventManager(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<PluginEventManager>();

            dic_EventTasks = new Dictionary<Guid, PluginEventTask>();
            dic_Plugins = new Dictionary<int, HandlerShell>();
        }
        #region IEventBus 实现
        public int Cancel<eType>(eType e) where eType : IPluginEvent
        {
            _logger.LogError($"IEventBus Cancel 尚未实现");

            return 0;
        }

        public void Publish<eType>(eType e) where eType : IPluginEvent
        {
            if (e == null)
            {
                _logger.LogWarning("IPluginEventManager.Publish 接收到了 null 事件");
                return;
            }

            _logger.LogDebug($"{e.FromPlugin} 发布事件：{e}");

            var eTask = new PluginEventTask(e);

            dic_EventTasks.Add(e.Uid, eTask);

            var toSymbols = e.ToPluginSymbol;

            var findPluginShells = from ps in dic_Plugins.Values
                                   from s in toSymbols
                                   where ps.Plugin.Symbol == s
                                   select ps;

            foreach (var ps in findPluginShells)
            {
                if (ps.IsHandleEvent(e.GetType()))
                {
                    Distribution(eTask, ps);
                }
            }

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
        /// 将事件分发给一个 handler ，如果当前没有 处理事件的 task ，那么启动一个新的 task 处理事件
        /// </summary>
        /// <param name="eventTask"></param>
        /// <param name="shell"></param>
        /// <returns></returns>
        private Task Distribution(PluginEventTask eventTask, HandlerShell shell)
        {
            return Task.Run(() =>
            {
                lock (shell.EventTasks)
                {
                    shell.EventTasks.Enqueue(eventTask);
                }

                lock (shell)
                {
                    if (!shell.HasDealEventTask)
                    {
                        shell.HasDealEventTask = true;

                        CreateDealTask(shell);
                    }
                }
            });
        }

        /// <summary>
        /// 启动一个 task 处理 shell 的事件
        /// </summary>
        /// <param name="shell"></param>
        /// <returns></returns>
        private Task CreateDealTask(HandlerShell shell)
        {
            return Task.Run(() =>
            {
                var running = true;

                while (running)
                {
                    PluginEventTask eTask = null;

                    lock (shell)
                    {
                        if (shell.EventTasks.Count > 0)
                        {
                            eTask = shell.EventTasks.Dequeue();
                        }
                        else
                        {
                            running = false;
                        }
                    }

                    if (eTask != null)
                        HandleEventTask(eTask, shell);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eTask"></param>
        /// <param name="shell"></param>
        private void HandleEventTask(PluginEventTask eTask, HandlerShell shell)
        {
            var e = eTask.PluginEvent;

            //TODO 添加对 PluginEventTask 中有关数量的处理

            shell.HandleEvent(e);
        }
    }
}
