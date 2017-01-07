using D.Spider.Core.SpiderscriptEngine;

namespace D.Spider.Core.SpiderscriptEngine
{
    /// <summary>
    /// Spiderscript 代码行
    /// </summary>
    internal class SsCodeLine
    {
        /// <summary>
        /// 行类型
        /// </summary>
        public SsKeywordTypes Type { get; set; }

        /// <summary>
        /// 代码块层级编号
        /// </summary>
        public int CodeBlockNumber { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int LineIndex { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string[] Cpdes { get; set; }
    }
}
