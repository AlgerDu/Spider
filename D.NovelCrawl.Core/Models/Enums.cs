using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models
{
    /// <summary>
    /// 爬取小说，处理小说页面的步骤类型
    /// 指示这一步应该用什么方式获取数据
    /// </summary>
    public enum PageProcessStepTypes
    {
        ///以 html 节点的方式获取数据
        Html,

        ///以正则表达式的方式获取数据
        RegExp
    }

    /// <summary>
    /// Url 的类型
    /// </summary>
    public enum UrlTypes
    {
        ///小说目录
        NovleCatalog,

        ///小说正文
        NovleChapterTxt
    }
}
