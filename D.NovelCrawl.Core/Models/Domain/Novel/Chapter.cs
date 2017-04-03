using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.Domain.Novel
{
    /// <summary>
    /// 小说领域内章节值对象
    /// </summary>
    public class Chapter
    {
        public Guid Uid { get; set; }

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
        public DateTime PublishTime { get; set; }

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

        /// <summary>
        /// 已上传
        /// </summary>
        public bool Uploaded { get; set; }

        /// <summary>
        /// 用来在上传完成之前保存文本数据，上传完成之后需要清空
        /// </summary>
        public string Text { get; set; }
    }
}
