using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 小说章节信息
    /// </summary>
    public class NovelChapterModel
    {
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
    }

    /// <summary>
    /// 小说卷信息
    /// </summary>
    public class NovelVolumeModel
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
        public NovelChapterModel[] Chapters { get; set; }
    }
}
