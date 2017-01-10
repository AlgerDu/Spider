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

    /// <summary>
    /// 关键字类型
    /// 这里的顺序是对解析有影响的
    /// </summary>
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
        /// 定义变量
        /// </summary>
        SsVar,

        /// <summary>
        /// 返回值
        /// </summary>
        SsReturn,

        /// <summary>
        /// 数组添加项
        /// </summary>
        SsArrayAddItem,

        /// <summary>
        /// 赋值
        /// </summary>
        SsSet
    }
}
