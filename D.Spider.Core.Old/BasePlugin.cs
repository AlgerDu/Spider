using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// Plugin 基类
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        protected IPluginSymbol _symbol;
        protected PluginState _state;
        protected bool _isRunning;

        public IPluginSymbol Symbol => _symbol;

        public PluginState State => _state;

        public bool IsRunning => _isRunning;

        public abstract IPlugin Run();

        public abstract IPlugin Stop();

        public override string ToString()
        {
            return _symbol.ToString();
        }

        protected void CreateSymbol(string name, PluginType pluginType)
        {
            _symbol = new PluginSymbol
            {
                Name = name,
                PType = pluginType
            };
        }
    }
}
