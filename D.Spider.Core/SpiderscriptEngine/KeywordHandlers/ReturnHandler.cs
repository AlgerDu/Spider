﻿using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine.KeywordHandlers
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
                var ws = line
                    .Replace("return", "")
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (ws.Length != 1)
                    throw new Exception("return 格式错误");

                return new SsCodeLine
                {
                    Type = SsKeywordTypes.SsReturn,
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
            //TODO
            context.ReturnObject = scope[line.Codes[0]];
            context.CodeExecuteFinish = true;
        }
    }
}
