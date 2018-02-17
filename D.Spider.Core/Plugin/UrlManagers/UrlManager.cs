using D.Spider.Core.Interface;
using D.Spider.Core.Plugin.UrlManagers;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Plugin
{
    ///1、现在默认只有一个下载器可以使用；后面可能需要有多个，或者应该说 manager 需要知道有几个下载器可以使用

    /// <summary>
    /// 内置的 url manager 类型的 plugin；
    /// 处理 url 队列
    /// </summary>
    public class UrlManager : BasePlugin, IPlugin
        , IPluginEventHandler<IUrlEvent>
    {
        ILogger _logger;

        IEventFactory _eventFactory;
        IEventBus _eventBus;

        /// <summary>
        /// 需要立即被爬取的 url
        /// </summary>
        IList<UrlCrawlTask> _toCrawlTasks;

        /// <summary>
        /// 正在进行的任务
        /// </summary>
        UrlCrawlTask _crawlingTask;

        public UrlManager(
            ILoggerFactory loggerFactory
            , IEventFactory eventFactory
            , IEventBus eventBus
            )
        {
            _logger = loggerFactory.CreateLogger<UrlManager>();

            _eventFactory = eventFactory;
            _eventBus = eventBus;

            CreateSymbol("Core.UrlManager", PluginType.UrlManager);

            _toCrawlTasks = new List<UrlCrawlTask>();
        }

        public void Handle(IUrlEvent e)
        {
            _logger.LogDebug($"url manager 接收到事件 {e.Uid}");

            var t = new UrlCrawlTask();

            t.CauseEvent = e;
            t.Url = e.ToCrawlUrl;
            t.ToCrawlTime = e.CrawlOptions.StartTime.Value;

            _toCrawlTasks.Add(t);

            StartIsNotRunningAnyTask();
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

        /// <summary>
        /// 如果没有任务正在执行，就开始执行
        /// </summary>
        /// <returns></returns>
        private Task StartIsNotRunningAnyTask()
        {
            return Task.Run(() =>
            {
                lock (this)
                {
                    if (_toCrawlTasks.Count > 0 && _crawlingTask == null)
                    {

                    }
                }
            });
        }
    }
}
