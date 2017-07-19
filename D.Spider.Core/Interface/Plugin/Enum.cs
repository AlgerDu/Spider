using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface.Plugin
{
    /// <summary>
    /// 插件类型
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// 下载器，只要负责下载 html
        /// </summary>
        Downloader,

        /// <summary>
        /// url 管理器
        /// </summary>
        UrlManager,

        /// <summary>
        /// 页面处理
        /// </summary>
        PageProcess
    }
}
