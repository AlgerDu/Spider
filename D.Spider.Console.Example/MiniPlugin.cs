using D.Spider.Core;
using D.Spider.Core.Event;
using D.Spider.Core.Extension;
using D.Spider.Core.Interface;
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
        , IPluginEventHandler<IPageDownloadEvent>
    {
        readonly Guid _uid = Guid.NewGuid();
        const string _exampleUrl = "www.bing.com";

        ILogger _logger;

        IPluginSymbol _symbol;
        PluginState _pluginState;

        IEventBus _eventBus;
        IEventFactory _eventFactory;

        public IPluginSymbol Symbol => _symbol;

        public PluginState State => _pluginState;

        public MiniPlugin(
            ILoggerFactory loggerFactory
            , IEventFactory eventFactory
            , IEventBus eventBus
            )
        {
            _logger = loggerFactory.CreateLogger<MiniPlugin>();

            _eventBus = eventBus;
            _eventFactory = eventFactory;

            _symbol = this.CreateSymbol(_uid, PluginType.PageProcess);
        }

        public IPlugin Run()
        {
            _logger.LogInformation($"{Symbol} run");

            var e = _eventFactory.CreateUrlEvent(_exampleUrl);

            _eventBus.Publish(e);

            return this;
        }

        public IPlugin Stop()
        {
            _logger.LogInformation($"{Symbol} stop");

            return this;
        }

        public void Handle(IPageDownloadEvent e)
        {
            _logger.LogInformation($"url 下载完成");
        }
    }
}
