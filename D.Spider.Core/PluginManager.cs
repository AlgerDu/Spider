using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class PluginManager : IPluginManager
    {
        ILogger _logger;

        IPluginCollecter _pluginCollecter;

        IPluginEventManager _pluginEventManager;

        Dictionary<string, Type> _dic_PluginTypes;
        IList<IPlugin> _list_plugins;

        bool _isRunning;

        public IEnumerable<IPlugin> Plugins => throw new NotImplementedException();

        public bool IsRunning => _isRunning;

        public PluginManager(
            ILoggerFactory loggerFactory
            , IPluginEventManager pluginEventManager
            , IPluginCollecter pluginCollecter
            )
        {
            _logger = loggerFactory.CreateLogger<PluginManager>();

            _pluginEventManager = pluginEventManager;
            _pluginCollecter = pluginCollecter;

            _dic_PluginTypes = new Dictionary<string, Type>();
            _list_plugins = new List<IPlugin>();

            _isRunning = false;
        }

        public void LoadAllPlugin()
        {
            var collectedTypes = _pluginCollecter.GetCollectedPluginType();
        }

        public IPluginManager Run()
        {
            _logger.LogInformation($"plugin manager run");

            _isRunning = true;

            foreach (var plugin in _list_plugins)
            {
                plugin.Run();
            }

            return this;
        }

        public IEnumerable<IPlugin> Search(IPluginSymbol symbol)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlugin> Search(IEnumerable<IPluginSymbol> symbols)
        {
            throw new NotImplementedException();
        }

        public IPluginManager Stop()
        {
            _isRunning = false;

            foreach (var plugin in _list_plugins)
            {
                plugin.Stop();
            }

            _logger.LogInformation($"plugin manager stop");

            return this;
        }
    }
}
