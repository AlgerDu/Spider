using Newtonsoft.Json.Linq;
using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (Regex.IsMatch(line, "var"))
            {
                var ws = line
                    .Replace("var", "")
                    .Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (ws.Length != 2)
                    throw new Exception("var 格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsVar,
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
            var name = line.Codes[0];

            switch (line.Codes[1])
            {
                case "object":
                    scope[name] = new SsVariable
                    {
                        Data = new JObject(),
                        Type = SsVariableTypes.SsObject
                    };
                    break;
                case "array":
                    scope[name] = new SsVariable
                    {
                        Data = new JArray(),
                        Type = SsVariableTypes.SsArray
                    };
                    break;
                default:
                    throw new Exception("var 定义了未知的变量类型 " + line.Codes[1]);
            }

            context.CurrDealLineIndex++;
        }
    }
}
