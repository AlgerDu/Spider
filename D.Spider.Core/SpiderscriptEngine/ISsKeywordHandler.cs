using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine
{
    /// <summary>
    /// ISpiderscriptEngine 关键字处理器 策略模式
    /// </summary>
    internal interface ISsKeywordHandler
    {
        /// <summary>
        /// handler 处理的关键字
        /// </summary>
        SsKeywordTypes Type { get; }

        /// <summary>
        /// 解析 code 字符串
        /// 不包含此类型关键字返回 null
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        SsCodeLine Analysis(string line);

        /// <summary>
        /// 执行代码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ele"></param>
        /// <param name="scope"></param>
        void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope);
    }
}
