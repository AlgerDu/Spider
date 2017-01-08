using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine
{
    /// <summary>
    /// ISpiderscriptEngine 执行代码上下文
    /// </summary>
    internal class SsContext
    {
        /// <summary>
        /// 代码行列表
        /// </summary>
        public SsCodeLines CodeLines { get; set; }

        /// <summary>
        /// 当前正在处理的代码行
        /// </summary>
        public int CurrDealLineIndex { get; set; }

        /// <summary>
        /// 代码执行完毕
        /// </summary>
        public bool CodeExecuteFinish { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnObject { get; set; }

        /// <summary>
        /// 根作用域
        /// </summary>
        public SsScope RootScope { get; set; }

        public Dictionary<SsKeywordTypes, ISsKeywordHandler> KeywordHandlers { get; set; }
    }
}
