using D.Spider.Core.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;

namespace D.Spider.Core
{
    /// <summary>
    /// ISpider 接口的实现
    /// </summary>
    public class DSpider : ISpider
    {
        IUnityContainer _unityContainer;

        IEventBus _eventBus;

        IUrlManager _urlManager;
        IPageProcess _pageProcess;
        IDownloader _downloader;

        public DSpider()
        {
            _unityContainer = new UnityContainer();
        }

        #region ISpider 属性

        public IUrlManager UrlManager { get { return _urlManager; } }

        public IUnityContainer UnityContainer { get { return _unityContainer; } }

        #endregion

        #region ISpider 接口
        public ISpider Run()
        {
            _eventBus.Subscribe(this);
            _downloader.Run();
            return this;
        }

        public ISpider Initialization()
        {
            _urlManager = _unityContainer.Resolve<IUrlManager>();
            _pageProcess = _unityContainer.Resolve<IPageProcess>();
            _downloader = _unityContainer.Resolve<IDownloader>();

            _eventBus = _unityContainer.Resolve<IEventBus>();

            return this;
        }

        public ISpider UnityConfigerPath(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("传入的 spider unity container 配置文件不存在");
            }

            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = path };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var section = (UnityConfigurationSection)configuration.GetSection("unity");
            _unityContainer.LoadConfiguration(section, "SpiderContainer");

            return this;
        }

        public void Handle(UrlCrawledEvent e)
        {
            _pageProcess.Process(e.Url);
        }
        #endregion
    }
}
