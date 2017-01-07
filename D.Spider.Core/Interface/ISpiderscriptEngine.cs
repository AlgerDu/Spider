using Newtonsoft.Json.Linq;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 简化版的 js 语言，专为自定义的爬虫使用
    /// </summary>
    public interface ISpiderscriptEngine
    {
        /// <summary>
        /// 运行 spiderscript 代码
        /// </summary>
        /// <param name="html">html doc 字符串</param>
        /// <param name="spiderscriptCode">spiderscript 代码字符串</param>
        /// <returns></returns>
        JToken Run(string html, string spiderscriptCode);
    }
}
