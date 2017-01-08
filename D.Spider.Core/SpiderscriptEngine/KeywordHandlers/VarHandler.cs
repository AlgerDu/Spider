using NSoup.Nodes;
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
            //TODO
            return null;
        }

        public void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope)
        {
            //TODO
        }
    }
}
