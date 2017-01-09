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
                var ws = line
                    .Replace("=", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (ws.Length != 2)
                    throw new Exception("赋值操作格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsSet,
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
            //v.Name = $('h3').inner
            //c.Name = $('a').attr('title')
            //cs[] = c

            var codes = line.Codes;

            if (Regex.IsMatch(codes[0], @"\."))
            {
                var ops = codes[0].Split('.');

                var toSet = scope[ops[0]];

                if (toSet == null)
                    throw new Exception(ops[0] + " 对象不存在");

                var op2s = codes[1].Split(new char[] { '$', '\'', '(', ')', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (op2s.Length == 1)
                {
                    (toSet.Data as JObject)[ops[1]] = scope[op2s[0]].Data as JToken;
                }
                else if (op2s[1] == "text")
                {
                    (toSet.Data as JObject)[ops[1]] = ele.Select(op2s[0]).Text;
                }
                else if (op2s[1] == "attr")
                {
                    (toSet.Data as JObject)[ops[1]] = ele.Select(op2s[0]).Attr(op2s[2]);
                }
                else
                {
                    throw new Exception("不支持的 " + codes[1]);
                }
            }
            else if (Regex.IsMatch(codes[0], @"\[\]"))
            {
                var toAdd = scope[codes[0].Replace("[]", "")];

                if (toAdd.Type != SsVariableTypes.SsArray)
                    throw new Exception(codes[0].Replace("[]", "") + " 不是一个 json 数组");

                var setVal = scope[codes[1]];

                if (setVal == null)
                    throw new Exception(codes + " 对象不存在");

                (toAdd.Data as JArray).Add(setVal.Data as JToken);
            }
            else
            {
                throw new Exception("不能处理赋值操作形式");
            }

            context.CurrDealLineIndex++;
        }
    }
}
