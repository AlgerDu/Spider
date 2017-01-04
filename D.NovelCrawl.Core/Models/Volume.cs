using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models
{
    /// <summary>
    /// 小说卷信息
    /// </summary>
    public class Volume
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 卷名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 章节信息
        /// </summary>
        public Dictionary<int, Chapter> Chapters { get; set; }

        public Volume()
        {
            Chapters = new Dictionary<int, Chapter>();
        }
    }
}
