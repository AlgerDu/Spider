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
        /// 字符串开始空格数
        /// </summary>
        public int SpaceCount { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int LineIndex { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string[] Codes { get; set; }
    }
}
