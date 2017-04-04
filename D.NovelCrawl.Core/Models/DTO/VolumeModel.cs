using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 卷信息 DTO
    /// </summary>
    public class VolumeModel
    {
        public Guid BookUid { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 卷名
        /// </summary>
        public string Name { get; set; }
    }
}
