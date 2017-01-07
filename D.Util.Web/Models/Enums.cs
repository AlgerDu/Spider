using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Models
{
    public enum SsVariableTypes
    {
        SsArray,
        SsObject
    }

    public enum SsCodeLineTypes
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
        SsDefVar
    }
}
