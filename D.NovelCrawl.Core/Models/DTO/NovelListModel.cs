using System;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 小说列表信息
    /// </summary>
    public class NovelListModel
    {
        /// <summary>
        /// 小说 GUID
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 小说名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        
        /// <summary>
        /// 已收录的章节总数 （包括一些单开章节）
        /// </summary>
        /// <returns></returns>
        public int ChapterCount { get; set; }

        /// <summary>
        /// 最新更新的章节名称
        /// </summary>
        /// <returns></returns>
        public string LastChapterName{get;set;}

        /// <summary>
        /// 最近更新一章的章节编号
        /// </summary>
        /// <returns></returns>
        public int LastChapterNumber{get;set;}
    }
}