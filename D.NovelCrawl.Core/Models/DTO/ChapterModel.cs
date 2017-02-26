using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 章节信息 DTO
    /// </summary>
    public class ChapterModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// 所属卷编号
        /// </summary>
        public int VolumeNo { get; set; }

        /// <summary>
        /// 卷内顺序号
        /// </summary>
        public int VolumeIndex { get; set; }

        /// <summary>
        /// 章节名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishCount { get; set; }

        /// <summary>
        /// 章节文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 是否 vip 章节
        /// </summary>
        public bool Vip { get; set; }

        /// <summary>
        /// 是否需要重新爬取
        /// </summary>
        public bool Recrawl { get; set; }

        /// <summary>
        /// 来源网页地址 上传时需要
        /// </summary>
        public string SourceUrl { get; set; }
    }
}
