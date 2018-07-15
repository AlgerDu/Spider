using Autofac;
using D.Spider.Core.Interface;
using Microsoft.Extensions.Logging;
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
        ILifetimeScope _containerScope;

        IPluginEventManager _pluginEventManager;

        Dictionary<string, Type> _dic_PluginTypes;
        IList<IPlugin> _list_plugins;

        bool _isRunning;
        int _pluginInstanceCount;

        public IEnumerable<IPlugin> Plugins => throw new NotImplementedException();

        public bool IsRunning => _isRunning;

        public PluginManager(
            ILogger<PluginManager> logger
            , IPluginEventManager pluginEventManager
            , IPluginCollecter pluginCollecter
            , ILifetimeScope containerScope
            )
        {
            _logger = logger;

            _pluginEventManager = pluginEventManager;
            _pluginCollecter = pluginCollecter;
            _containerScope = containerScope;

            _dic_PluginTypes = new Dictionary<string, Type>();
            _list_plugins = new List<IPlugin>();

            _isRunning = false;
            _pluginInstanceCount = 0;
        }

        public void LoadAllPlugin()
        {
            _logger.LogInformation($"开始加载可以获取到的所有类型的插件");

            var collectedTypes = _pluginCollecter.GetCollectedPluginType();

            foreach (var pluginType in collectedTypes)
            {
                if (_dic_PluginTypes.ContainsKey(pluginType.FullName))
                {
                    _logger.LogWarning($"插件类型 {pluginType.FullName} 已经加载");
                }
                else
                {
                    _dic_PluginTypes.Add(pluginType.FullName, pluginType);
                    _logger.LogInformation($"将插件类型 {pluginType.FullName} 加载到 plugin manager 中");
                }
            }

            CreatePluginInstance();
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

        /// <summary>
        /// 通过 ILifetimeScope 注入所有收集到的 Plugin 类型，并且全部生成实例
        /// </summary>
        private void CreatePluginInstance()
        {
            using (var scope = _containerScope.BeginLifetimeScope(new Action<ContainerBuilder>(SocpeBuilder)))
            {
                var plugins = scope.Resolve<IEnumerable<IPlugin>>();

                plugins = plugins.OrderBy(pp => pp.Symbol?.PType);

                foreach (var plugin in plugins)
                {
                    DealNewPluginInstance(plugin);

                    _logger.LogInformation($"创建插件实例 {plugin}");

                    _list_plugins.Add(plugin);
                    _pluginEventManager.AddPlugin(plugin);
                }
            }
        }

        /// <summary>
        /// autofac ILifetimeScope BeginLifetimeScope 使用的 action
        /// </summary>
        /// <param name="builder"></param>
        private void SocpeBuilder(ContainerBuilder builder)
        {
            foreach (var t in _dic_PluginTypes.Values)
            {
                builder.RegisterType(t)
                    //.Named<IPlugin>(t.FullName);
                    .AsSelf()
                    .As<IPlugin>();
            }
        }

        /// <summary>
        /// 对新生成的插件实例进行一些处理
        /// </summary>
        /// <param name="plugin"></param>
        private void DealNewPluginInstance(IPlugin plugin)
        {
            //TODO 后面需要修改
            _pluginInstanceCount++;
            (plugin.Symbol as PluginSymbol).InstanceID = _pluginInstanceCount;
        }
    }
}
