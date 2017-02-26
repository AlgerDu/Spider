using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 小说目录结构信息 DTO
    /// </summary>
    public class NovelCatalogModel
    {
        /// <summary>
        /// 所有卷信息
        /// </summary>
        public VolumeModel[] Vs { get; set; }

        /// <summary>
        /// 所有章节信息
        /// </summary>
        public ChapterModel[] Cs { get; set; }

        public NovelCatalogModel()
        {
            Vs = new VolumeModel[0];
            Cs = new ChapterModel[0];
        }
    }
}
