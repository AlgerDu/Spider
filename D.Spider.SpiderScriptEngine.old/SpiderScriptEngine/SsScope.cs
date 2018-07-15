using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderScriptEngine
{
    /// <summary>
    /// SpiderscriptEngine 作用域
    /// </summary>
    internal class SsScope
    {
        /// <summary>
        /// 父作用域
        /// </summary>
        public SsScope Parent { get; private set; }

        /// <summary>
        /// 保存的变量
        /// </summary>
        public Dictionary<string, SsVariable> Variables { get; private set; }

        public SsVariable this[string name]
        {
            get
            {
                lock (this)
                {
                    if (Variables.ContainsKey(name))
                    {
                        return Variables[name];
                    }
                    else if (Parent != null)
                    {
                        return Parent[name];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set
            {
                lock (this)
                {
                    if (Variables.ContainsKey(name))
                    {
                        Variables[name] = value;
                    }
                    else
                    {
                        Variables.Add(name, value);
                    }
                }
            }
        }

        public SsScope()
        {
            Parent = null;
            Variables = new Dictionary<string, SsVariable>();
        }

        public SsScope(SsScope parent)
        {
            Parent = parent;
            Variables = new Dictionary<string, SsVariable>();
        }
    }
}
