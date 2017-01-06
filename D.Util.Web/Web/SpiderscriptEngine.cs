using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace D.Util.Web
{
    /// <summary>
    /// ISpiderscriptEngine 实现
    /// </summary>
    public class SpiderscriptEngine : ISpiderscriptEngine
    {
        public JToken Run(string doc, string spiderscriptCode)
        {
            return new JObject();
        }
    }
}
