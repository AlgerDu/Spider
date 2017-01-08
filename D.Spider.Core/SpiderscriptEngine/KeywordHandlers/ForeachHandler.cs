using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSoup.Nodes;
using System.Text.RegularExpressions;

namespace D.Spider.Core.SpiderscriptEngine.KeywordHandlers
{
    internal class ForeachHandler : ISsKeywordHandler
    {
        public SsKeywordTypes Type
        {
            get
            {
                return SsKeywordTypes.SsForeach;
            }
        }

        public SsCodeLine Analysis(string line)
        {
            if (Regex.IsMatch(line, "foreach"))
            {
                var ws = line
                    .Replace("foreach", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (ws.Length != 1)
                    throw new Exception("foreach 格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsForeach,
                    Codes = ws
                };
            }
            else
            {
                return null;
            }
        }

        public void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope)
        {
            Regex r = new Regex(@"(?<=\$\(')[^']*(?='\))");
            var m = r.Match(line.Codes[0]);

            var index = context.CurrDealLineIndex;

            context.CurrDealLineIndex++;

            foreach (var e in ele.Select(m.Value))
            {
                var childScope = new SsScope(scope);

                while (!context.CodeExecuteFinish)
                {
                    var nline = context.CodeLines[context.CurrDealLineIndex];

                    if (nline.SpaceCount < line.SpaceCount)
                        break;

                    context.KeywordHandlers[line.Type].Execute(context, line, e, childScope);
                }
            }
        }
    }
}
