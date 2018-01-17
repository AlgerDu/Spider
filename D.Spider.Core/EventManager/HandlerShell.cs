using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.EventManager
{
    /// <summary>
    /// event handler 的一个壳，
    /// 分析 plugin handle 了哪些事件，调用 plugin 执行事件
    /// </summary>
    internal class HandlerShell
    {
        /// <summary>
        /// plugin 能够处理的 event 类型 fullName 集合
        /// </summary>
        Dictionary<Type, MethodInfo> _handleTpes;

        HashSet<string> _unhandleTypes;

        /// <summary>
        /// shell 里面包含的 plugin
        /// </summary>
        public IPlugin Plugin { get; private set; }

        /// <summary>
        /// plugin 需要处理的 event 队列
        /// </summary>
        public Queue<PluginEventTask> EventTasks { get; set; }

        /// <summary>
        /// 是否有正在处理 event 的 task
        /// </summary>
        public bool HasDealEventTask { get; set; }

        public int Key => Plugin.Symbol.InstanceID.Value;

        public HandlerShell(IPlugin plugin)
        {
            if (plugin == null)
            {
                throw new Exception("handler shell plugin is null");
            }

            _handleTpes = new Dictionary<Type, MethodInfo>();
            _unhandleTypes = new HashSet<string>();

            HasDealEventTask = false;
            EventTasks = new Queue<PluginEventTask>();

            Plugin = plugin;

            AnalysisHandler();
        }

        /// <summary>
        /// plugin 是否处理 t 类型的 event
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsHandleEvent(Type t)
        {
            if (_handleTpes.ContainsKey(t))
            {
                return true;
            }
            else if (_unhandleTypes.Contains(t.FullName))
            {
                return false;
            }
            else
            {
                foreach (var handleType in _handleTpes.Keys)
                {
                    if (handleType.IsAssignableFrom(t))
                    {
                        _handleTpes.Add(t, _handleTpes[handleType]);

                        return true;
                    }
                }

                _unhandleTypes.Add(t.FullName);

                return false;
            }
        }

        public void HandleEvent(IPluginEvent e)
        {
            var eType = e.GetType();

            if (_handleTpes.ContainsKey(eType))
            {
                var m = _handleTpes[eType];

                m.Invoke(Plugin, new object[] { e });
            }
            else
            {
                throw new Exception($"{Plugin} 不能处理事件 {e}");
            }
        }

        /// <summary>
        /// 解析 plugin 可以处理的 event 类型
        /// </summary>
        private void AnalysisHandler()
        {
            var pType = Plugin.GetType();

            var handleMemberInfos = pType.GetMethods();

            foreach (var m in handleMemberInfos)
            {
                if (m.Name == "Handle")
                {
                    var paras = m.GetParameters();

                    if (paras.Count() == 1)
                    {
                        var p = paras.FirstOrDefault();

                        _handleTpes.Add(p.ParameterType, m);
                    }
                }
            }
        }
    }
}
