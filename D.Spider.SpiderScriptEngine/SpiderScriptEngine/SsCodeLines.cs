using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderScriptEngine
{
    internal class SsCodeLines
    {
        Dictionary<int, SsCodeLine> _lines;

        public bool Finish
        {
            get
            {
                return CurrDealIndex >= Count;
            }

            set
            {
                CurrDealIndex = Count;
            }
        }

        /// <summary>
        /// 当前需要处理的代码行
        /// </summary>
        public int CurrDealIndex { get; set; }

        public int Count
        {
            get
            {
                return _lines.Count;
            }
        }

        public SsCodeLines()
        {
            _lines = new Dictionary<int, SsCodeLine>();
        }

        ~SsCodeLines()
        {
            Clear();
        }

        /// <summary>
        /// 返回值的变量名称
        /// </summary>
        public string RuturnVarName { get; set; }

        /// <summary>
        /// 获取某一行的代码
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SsCodeLine this[int index]
        {
            get
            {
                if (_lines.ContainsKey(index))
                {
                    return _lines[index];
                }
                else
                {
                    return null;
                }
            }
        }

        public void Add(SsCodeLine item)
        {
            if (_lines.ContainsKey(item.LineIndex))
            {
                throw new Exception("添加了相同行的代码");
            }
            else
            {
                _lines.Add(item.LineIndex, item);
            }
        }

        public void Clear()
        {
            _lines.Clear();
        }
    }
}
