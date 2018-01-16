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
    public class MiniPlugin : BasePlugin, IPlugin
        , IPluginEventHandler<IPageDownloadEvent>
    {
        const string _name = "example";
        const string _exampleUrl = "www.bing.com";

        ILogger _logger;

        IEventBus _eventBus;
        IEventFactory _eventFactory;

        public MiniPlugin(
            ILoggerFactory loggerFactory
            , IEventFactory eventFactory
            , IEventBus eventBus
            )
        {
            _logger = loggerFactory.CreateLogger<MiniPlugin>();

            _eventBus = eventBus;
            _eventFactory = eventFactory;

            _isRunning = false;

            CreateSymbol(_name, PluginType.PageProcess);
        }

        public override IPlugin Run()
        {
            _logger.LogInformation($"{this} run");

            var e = _eventFactory.CreateUrlEvent(_exampleUrl);

            _eventBus.Publish(e);

            return this;
        }

        public override IPlugin Stop()
        {
            _logger.LogInformation($"{this} stop");

            return this;
        }

        public void Handle(IPageDownloadEvent e)
        {
            _logger.LogInformation($"url 下载完成");
        }
    }
}
