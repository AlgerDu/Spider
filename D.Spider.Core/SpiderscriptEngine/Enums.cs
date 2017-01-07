using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.SpiderscriptEngine
{
    public enum SsVariableTypes
    {
        SsArray,
        SsObject
    }

    public enum SsKeywordTypes
    {
        /// <summary>
        /// if 语句
        /// </summary>
        SsIf,

        /// <summary>
        /// foreach 语句
        /// </summary>
        SsForeach,

        /// <summary>
        /// 赋值
        /// </summary>
        SsSet,

        /// <summary>
        /// 定义变量
        /// </summary>
        SsVar,

        /// <summary>
        /// 返回值
        /// </summary>
        SsReturn
    }
}
