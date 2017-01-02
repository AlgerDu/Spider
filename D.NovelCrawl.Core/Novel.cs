using D.NovelCrawl.Core.DTO;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬虫需要的所有信息
    /// </summary>
    class Novel
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
        public string LastChapterName { get; set; }

        /// <summary>
        /// 最近更新一章的章节编号
        /// </summary>
        /// <returns></returns>
        public int LastChapterNumber { get; set; }

        /// <summary>
        /// 官网目录 Url
        /// </summary>
        public IUrl OfficialUrl { get; set; }

        /// <summary>
        /// 根据从个人网站上获取的小说信息更新爬虫持有的小说信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(NovelListModel model)
        {
            Guid = model.Guid;
            Name = model.Name;
            ChapterCount = model.ChapterCount;
            LastChapterName = model.LastChapterName;
            LastChapterNumber = model.LastChapterNumber;
        }
    }
}
