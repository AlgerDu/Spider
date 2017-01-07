using D.Spider.Core.SpiderscriptEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine
{
    /// <summary>
    /// Spiderscript 变量
    /// </summary>
    internal class SsVariable
    {
        public SsVariableTypes Type { get; set; }

        public object Data { get; set; }
    }
}
