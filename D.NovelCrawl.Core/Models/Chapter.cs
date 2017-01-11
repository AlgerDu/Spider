using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models
{
    /// <summary>
    /// 章节信息
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// 所属的卷编号
        /// </summary>
        public int VolumeNumber { get; set; }

        /// <summary>
        /// 在卷内的编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 章节号（可能为空）
        /// </summary>
        public int? ChapterNO { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublicTime { get; set; }

        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// 需要重新爬取
        /// </summary>
        public bool ReCrawl { get; set; }

        /// <summary>
        /// 是否vip章节
        /// </summary>
        public bool VipChapter { get; set; }

        /// <summary>
        /// 来源 Url
        /// </summary>
        public string SourceUrl { get; set; }
    }
}
