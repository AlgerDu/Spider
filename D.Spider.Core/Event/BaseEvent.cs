using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    public class BaseEvent : IPluginEvent
    {
        IList<IPluginSymbol> _toPluginSymbols;

        public Guid Uid { get; set; }

        public IPluginSymbol FromPlugin { get; set; }

        public IEnumerable<IPluginSymbol> ToPluginSymbols => _toPluginSymbols;

        public DateTime CreateTime { get; set; }

        public PluginEventState State { get; set; }

        public DealPluginEventType DealType { get; set; }

        public BaseEvent()
        {
            Uid = Guid.NewGuid();
            CreateTime = DateTime.Now;
            State = PluginEventState.Created;

            _toPluginSymbols = new List<IPluginSymbol>();
        }

        public void AddToPluginSymbol(IPluginSymbol symbol)
        {
            if (symbol != null)
                _toPluginSymbols.Add(symbol);
        }
    }
}
