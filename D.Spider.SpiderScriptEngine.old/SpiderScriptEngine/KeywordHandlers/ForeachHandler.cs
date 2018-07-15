using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSoup.Nodes;
using System.Text.RegularExpressions;
using D.Spider.Core.Interface;

namespace D.Spider.Core.SpiderScriptEngine.KeywordHandlers
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
                var result = line
                    .Replace("foreach", "");
                //.Split(new char[] { '\'', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                var m = Regex.Match(result, @"(?<=\(')[^']*(?='\))");

                //if (result.Length != 1)
                //    throw new Exception("foreach 格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsForeach,
                    LCodes = new string[] { m.Value }
                };
            }
            else
            {
                return null;
            }
        }

        public void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope)
        {
            //Regex r = new Regex(@"(?<=\$\(')[^']*(?='\))");
            //var m = r.Match(line.LCodes[0]);

            context.CurrDealLineIndex++;

            var backIndex = context.CurrDealLineIndex;

            foreach (var e in ele.Select(line.LCodes[0]))
            {
                context.CurrDealLineIndex = backIndex;

                var childScope = new SsScope(scope);

                while (!context.CodeExecuteFinish)
                {
                    var nline = context.CodeLines[context.CurrDealLineIndex];

                    if (nline.SpaceCount <= line.SpaceCount)
                        break;

                    context.KeywordHandlers[nline.Type].Execute(context, nline, e, childScope);
                }
            }
        }
    }
}
