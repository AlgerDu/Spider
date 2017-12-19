using D.Spider.Core.Interface.Plugin;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Example
{
    /// <summary>
    /// 最简单插件
    /// </summary>
    class MiniPlugin : IPlugin
    {
        ILogger _logger;

        IPluginSymbol _symbol;
        PluginState _pluginState;

        public IPluginSymbol Symbol => _symbol;

        public PluginState State => _pluginState;

        public MiniPlugin(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<MiniPlugin>();
        }

        public IPlugin Run()
        {
            _logger.LogInformation($"{Symbol} run");

            return this;
        }

        public IPlugin Stop()
        {
            _logger.LogInformation($"{Symbol} stop");

            return this;
        }
    }
}
