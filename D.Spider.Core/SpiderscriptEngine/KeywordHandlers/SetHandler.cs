using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSoup.Nodes;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace D.Spider.Core.SpiderscriptEngine.KeywordHandlers
{
    /// <summary>
    /// 赋值处理
    /// </summary>
    internal class SetHandler : ISsKeywordHandler
    {
        public SsKeywordTypes Type
        {
            get
            {
                return SsKeywordTypes.SsSet;
            }
        }

        public SsCodeLine Analysis(string line)
        {
            if (Regex.IsMatch(line, " = "))
            {
                var result = line
                    .Replace("=", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (result.Length != 2)
                    throw new Exception("赋值操作格式错误");

                var rms = Regex.Matches(result[1], @"(?<=\(')[^']*(?='\))|(?<=\.)[a-z]*|^[a-z]+");
                var rc = new string[rms.Count];
                for (var i = 0; i < rms.Count; i++)
                {
                    rc[i] = rms[i].Value;
                }

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsSet,
                    LCodes = result[0].Split('.'),
                    RCodes = rc
                };
            }
            else
            {
                return null;
            }
        }

        public void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope)
        {
            var lcodes = line.LCodes;
            var rcodes = line.RCodes;

            var toSet = scope[lcodes[0]];
            if (toSet == null)
                throw new Exception(lcodes[0] + " 对象不存在");

            if (rcodes.Length == 1)
            {
                var setVal = scope[rcodes[0]];

                if (lcodes.Length == 2)
                {
                    (toSet.Data as JObject)[lcodes[1]] = setVal.Data as JToken;
                }
                else
                {
                    toSet.Data = setVal.Data as JToken;
                }
            }
            else if (rcodes.Length == 2)
            {
                if (rcodes[1] == "text")
                {
                    (toSet.Data as JObject)[lcodes[1]] = ele.Select(rcodes[0]).Text;
                }
            }
            else if (rcodes.Length == 3)
            {
                if (rcodes[1] == "attr")
                {
                    (toSet.Data as JObject)[lcodes[1]] = ele.Select(rcodes[0]).Attr(rcodes[2]); ;
                }
            }
            else
            {
                throw new Exception("不能处理赋值操作形式");
            }

            context.CurrDealLineIndex++;
        }
    }
}
