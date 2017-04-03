using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 上传章节正文信息
    /// </summary>
    public class ChapterTxtUploadModel
    {
        /// <summary>
        /// 章节编号
        /// </summary>
        public Guid CUid { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }
    }
}
