using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Plugin
{
    /// <summary>
    /// 内置的 url manager 类型的 plugin；
    /// 处理 url 队列
    /// </summary>
    public class UrlManager : BasePlugin, IPlugin
        , IPluginEventHandler<IUrlEvent>
    {
        ILogger _logger;

        public UrlManager(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<UrlManager>();

            CreateSymbol("Core.UrlManager", PluginType.UrlManager);
        }

        public void Handle(IUrlEvent e)
        {
            _logger.LogDebug($"url manager 接收到事件 {e.Uid}");
        }

        public override IPlugin Run()
        {
            _isRunning = true;

            return this;
        }

        public override IPlugin Stop()
        {
            _isRunning = false;

            return this;
        }
    }
}
