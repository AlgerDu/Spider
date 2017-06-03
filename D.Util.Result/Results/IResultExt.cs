using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Results
{
    public static class IResultExt
    {
        /// <summary>
        /// 是否为成功结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool Success(this IResult result)
        {
            return result.Code == 0;
        }
    }
}
