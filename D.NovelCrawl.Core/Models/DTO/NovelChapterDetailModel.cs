using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 小说章节详细信息
    /// </summary>
    public class NovelChapterDetailModel
    {
        /// <summary>
        /// 所属小说的 Guid
        /// </summary>
        public Guid NovelGuid { get; set; }

        /// <summary>
        /// 章节 Guid
        /// </summary>
        public Guid ChapterGuid { get; set; }

        /// <summary>
        /// 所属卷编号
        /// </summary>
        public int VolumeNumber { get; set; }

        /// <summary>
        /// 卷名称
        /// </summary>
        public string VolumeName { get; set; }

        /// <summary>
        /// 章节在卷中的编号
        /// </summary>
        public int ChapterNumber { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public string ChapterTxt { get; set; }

        /// <summary>
        /// 章节编号（作者一些单开的章节为 NULL）
        /// </summary>
        public int? ChapterNO { get; set; }

        public override string ToString()
        {
            return string.Format(
                "第 {0} 卷 第 {1} 章 {2} ： {3}",
                VolumeNumber,
                VolumeName,
                ChapterNumber,
                ChapterName,
                ChapterTxt);
        }
    }
}
