using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// 内置的一个 插件事件 任务，用于对事件的一个简单外观封装，供 PluginEventManager 使用
    /// </summary>
    internal class PluginEventTask
    {
        IPluginEvent _event;

        Int32 _executing;
        Int32 _executed;
        Int32 _cancel;

        /// <summary>
        /// 任务 ID，实际是 event ID
        /// </summary>
        public Guid Uid { get => _event.Uid; }

        public IPluginEvent PluginEvent { get => _event; }

        /// <summary>
        /// 事件一共分配给了几个插件执行
        /// </summary>
        public Int32 DistributeCount { get; set; }

        /// <summary>
        /// 正在执行此事件的插件个数
        /// </summary>
        public Int32 ExecutingCount
        {
            get => _executing;
            set
            {
                lock (this) _executing = value;
            }
        }

        /// <summary>
        /// 执行完成的个数
        /// </summary>
        public Int32 ExecutedCount
        {
            get => _executed;
            set
            {
                lock (this) _executed = value;
            }
        }

        /// <summary>
        /// 取消执行的插件个数
        /// </summary>
        public Int32 CancelCount
        {
            get => _cancel;
            set
            {
                lock (this) _cancel = value;
            }
        }

        public PluginEventTask(IPluginEvent e)
        {
            _event = e;

            _cancel = 0;
            _executed = 0;
            _executing = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
