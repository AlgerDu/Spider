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
            else if (rcodes.Length >= 2 && rcodes.Length <= 5)
            {
                var index = 0;
                var e = ele.Select(rcodes[0]);
                var val = string.Empty;

                if (rcodes[1] == "text")
                {
                    val = e.Text;
                    index = 2;
                }
                else if (rcodes[1] == "attr")
                {
                    val = e.Attr(rcodes[2]);
                    index = 3;
                }

                if (rcodes.Length <= index)
                {
                    (toSet.Data as JObject)[lcodes[1]] = val;
                }
                else if (rcodes[index] == "regex")
                {
                    var r = new Regex(rcodes[index + 1]);
                    var m = r.Match(val);

                    var names = r.GetGroupNames();
                    for (var i = 1; i < names.Length; i++)
                    {
                        (toSet.Data as JObject)[names[i]] = m.Groups[names[i]].Value;
                    }
                }
                else
                {
                    throw new Exception("不能处理赋值操作形式");
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
