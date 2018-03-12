using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Plugin.UrlManagers
{
    /// <summary>
    /// Url 爬取任务
    /// </summary>
    internal class UrlCrawlTask
    {
        /// <summary>
        /// 需要爬去的 url
        /// </summary>
        public IUrl Url { get; set; }

        /// <summary>
        /// 将要爬去的时间
        /// </summary>
        public DateTime ToCrawlTime { get; set; }

        /// <summary>
        /// 这里本来想设计成一个 list，用来存储当有多个事件同时需要爬去一个 url 的时候，
        /// 可以减少下载器的下载次数，
        /// 但是仔细思考之后觉得这个暂时没有什么用处，修改成一个。
        /// 请原谅我英语不好，起不了满意的名字
        /// </summary>
        public IUrlCrawlEvent CauseEvent { get; set; }
    }
}
