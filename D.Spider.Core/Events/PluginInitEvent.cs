using D.Spider.Core.Interface;
using D.Spider.Core.Interface.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Events
{
    /// <summary>
    /// 插件初始化事件
    /// </summary>
    public class PluginInitEvent : IPluginEvent
    {
        public Guid TypeUid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string TypeName => throw new NotImplementedException();

        public IPluginSymbol FromPlugin => throw new NotImplementedException();

        public IPluginSymbol ToPluginSymbol => throw new NotImplementedException();

        public DateTime CreateTime => throw new NotImplementedException();

        public PluginEventState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
