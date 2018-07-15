using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSoup.Nodes;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using D.Spider.Core.Interface;

namespace D.Spider.Core.SpiderScriptEngine.KeywordHandlers
{
    /// <summary>
    /// 像数组添加 item
    /// </summary>
    internal class ArrayAddHandler : ISsKeywordHandler
    {
        public SsKeywordTypes Type
        {
            get
            {
                return SsKeywordTypes.SsArrayAddItem;
            }
        }

        public SsCodeLine Analysis(string line)
        {
            if (Regex.IsMatch(line, @"\[] = "))
            {
                var result = line
                    .Replace("[] =", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (result.Length != 2)
                    throw new Exception("赋值操作格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsArrayAddItem,
                    LCodes = result
                };
            }
            else
            {
                return null;
            }
        }

        public void Execute(SsContext context, SsCodeLine line, Element ele, SsScope scope)
        {
            var codes = line.LCodes;
            var toAdd = scope[codes[0]];

            if (toAdd.Type != SsVariableTypes.SsArray)
                throw new Exception(codes[0] + " 不是一个 json 数组");

            var setVal = scope[codes[1]];

            if (setVal == null)
                throw new Exception(codes + " 对象不存在");

            (toAdd.Data as JArray).Add(setVal.Data as JToken);

            context.CurrDealLineIndex++;
        }
    }
}
