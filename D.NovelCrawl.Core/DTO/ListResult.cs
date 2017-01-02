using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.DTO
{
    public class ListResult<T>
        where T : class, new()
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 当前页数据
        /// </summary>
        public IEnumerable<T> CurrPageData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="total">总数</param>
        /// <param name="size">每页大小</param>
        /// <param name="number">页码</param>
        /// <param name="data">数据</param>
        public ListResult(int total, int size, int number, IEnumerable<T> data)
        {
            RecordCount = total;
            PageSize = size;
            PageNumber = number;
            CurrPageData = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public ListResult()
        {
            RecordCount = 0;
            PageNumber = -1;
        }
    }
}
