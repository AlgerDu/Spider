using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Event
{
    /// <summary>
    /// IEventBus 的一个实现
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// 记录所有的事件订阅
        /// </summary>
        Dictionary<Type, List<object>> _handlers = new Dictionary<Type, List<object>>();

        public async Task<int> Publish<Event>(Event e) where Event : class, IEvent
        {
            return await Task.Run(() =>
            {
                var eType = e.GetType();
                if (_handlers.ContainsKey(eType))
                {
                    var all = _handlers[eType];

                    foreach (var handler in all)
                    {
                        (handler as IEventHandler<Event>).Handle(e);
                    }

                    return all.Count;
                }
                else
                {
                    return 0;
                }
            });

        }

        public void Subscribe<Event>(IEventHandler<Event> handler) where Event : class, IEvent
        {
            var eType = typeof(Event);

            if (_handlers.ContainsKey(eType))
            {
                _handlers[eType].Add(handler);
            }
            else
            {
                var all = new List<object>();
                all.Add(handler);
                _handlers.Add(eType, all);
            }
        }
    }
}
