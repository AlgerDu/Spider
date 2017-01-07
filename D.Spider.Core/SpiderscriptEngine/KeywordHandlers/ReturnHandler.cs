using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine.KeywordHandlers
{
    /// <summary>
    /// 处理 return 关键字
    /// </summary>
    internal class ReturnHandler : ISsKeywordHandler
    {
        public SsKeywordTypes Type
        {
            get
            {
                return SsKeywordTypes.SsReturn;
            }
        }

        public SsCodeLine Analysis(string line)
        {
            throw new NotImplementedException();
        }

        public void Execute(SsCodeLines lines, int index, SsScope scope)
        {
            throw new NotImplementedException();
        }
    }
}
