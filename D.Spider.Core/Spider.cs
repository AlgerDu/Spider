using D.Spider.Core.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace D.Spider.Core
{
    /// <summary>
    /// ISpider 接口的实现
    /// </summary>
    public class DSpider : ISpider
    {
        ILogger _logger;

        IPluginManager _pluginManager;
        IPluginEventManager _pluginEventManager;

        public bool IsRunning => throw new NotImplementedException();

        public DSpider(
            ILogger<DSpider> logger
            , IPluginManager pluginManager
            , IPluginEventManager pluginEventManager
            )
        {
            _logger = logger;

            _pluginManager = pluginManager;
            _pluginEventManager = pluginEventManager;
        }

        public ISpider Run()
        {
            _logger.LogInformation("spider 开始运行");

            _pluginEventManager.Run();

            _pluginManager.LoadAllPlugin();
            _pluginManager.Run();

            return this;
        }

        public ISpider Stop()
        {
            _pluginEventManager.Stop();
            _pluginManager.Stop();

            _logger.LogInformation("spider 停止");

            return this;
        }
    }
}
