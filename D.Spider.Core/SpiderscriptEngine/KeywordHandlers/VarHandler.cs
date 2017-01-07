using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine.KeywordHandlers
{
    internal class VarHandler : ISsKeywordHandler
    {
        public SsKeywordTypes Type
        {
            get
            {
                return SsKeywordTypes.SsVar;
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
