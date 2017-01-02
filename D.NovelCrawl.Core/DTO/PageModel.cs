using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.DTO
{
    /// <summary>
    /// 通用分页 dto 用来做分页过滤的基类，形成统一的分页操作
    /// </summary>
    public class PageModel
    {
        ///页码
        public int Number { get; set; }

        ///每页大小
        public int Size { get; set; }
    }
}
