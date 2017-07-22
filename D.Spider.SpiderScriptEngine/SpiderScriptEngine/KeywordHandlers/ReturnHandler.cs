using D.Spider.Core.Interface;
using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderScriptEngine.KeywordHandlers
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
            if (Regex.IsMatch(line, "return"))
            {
                var result = line
                    .Replace("return", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (result.Length != 1)
                    throw new Exception("return 格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsReturn,
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
            //TODO
            context.ReturnObject = scope[line.LCodes[0]];
            context.CodeExecuteFinish = true;
        }
    }
}
