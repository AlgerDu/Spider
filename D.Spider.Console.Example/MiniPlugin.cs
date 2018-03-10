using D.Spider.Core;
using D.Spider.Core.Extension;
using D.Spider.Core.Interface;
using D.Spider.Core.SpiderScriptEngine;
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
        , IPluginEventHandler<IUrlCrawledEvent>
    {
        const string _name = "example";
        const string _exampleUrl = "https://book.qidian.com/info/3602691#Catalog";

        ILogger _logger;

        IEventBus _eventBus;
        IEventFactory _eventFactory;

        ISpiderScriptEngine _ssEngine;

        public MiniPlugin(
            ILoggerFactory loggerFactory
            , IEventFactory eventFactory
            , IEventBus eventBus
            , ISpiderScriptEngine spiderScriptEngine
            )
        {
            _logger = loggerFactory.CreateLogger<MiniPlugin>();

            _eventBus = eventBus;
            _eventFactory = eventFactory;

            _ssEngine = spiderScriptEngine;

            _isRunning = false;

            CreateSymbol(_name, PluginType.PageProcess);
        }

        public override IPlugin Run()
        {
            _logger.LogInformation($"{this} run");

            var e = _eventFactory.CreateUrlEvent(this, _exampleUrl);

            _eventBus.Publish(e);

            return this;
        }

        public override IPlugin Stop()
        {
            _logger.LogInformation($"{this} stop");

            return this;
        }

        public void Handle(IUrlCrawledEvent e)
        {
            _logger.LogInformation($"{_exampleUrl} 下载完成，前 20 个字符是：{e.Page.HtmlTxt.Substring(0, 20)}");

            var script = @"
                    var vs:array
                    foreach $('div.catalog-content-wrap div.volume-wrap div.volume')
                        var v:object
                        v.Name = $('h3').text
                        var cs:array
                        foreach $('li')
                            var c:object
                            c = $('a').attr('title').regex('时间：(?<PublicTime>[\s\S]*?)章节字数：(?<WordCount>[\d]{0,5})')
                            c.Name = $('a').text
                            c = $('a').attr('href').regex('(?<Vip>vip)')
                            c.Href = $('a').attr('href')
                            cs[] = c
                        v.Chapters = cs
                        vs[] = v
                    return vs";

            var data = _ssEngine.Run(e.Page.HtmlTxt, script);

            _logger.LogInformation($"{data}");
        }
    }
}
