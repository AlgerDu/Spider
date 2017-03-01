using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    //此处放一些通用代码的定义，比如一些回调函数等的定义

    /// <summary>
    /// 判断一个 url 对应的页面是否下载完毕
    /// 创建 url 时赋值，由 IDownloader 使用
    /// </summary>
    /// <param name="url">下载的 url</param>
    /// <returns></returns>
    public delegate bool PageDownloadCompleteHandler(IUrl url);
}
