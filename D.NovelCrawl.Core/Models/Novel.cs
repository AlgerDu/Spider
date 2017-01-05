using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Models.DTO;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;

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
        /// 卷信息
        /// </summary>
        public Dictionary<int, Volume> Volumes { get; set; }

        /// <summary>
        /// 官网目录 Url
        /// </summary>
        public IUrl OfficialUrl { get; set; }

        public Novel()
        {
            Volumes = new Dictionary<int, Volume>();
        }

        /// <summary>
        /// 根据从个人网站上获取的小说信息更新爬虫持有的小说信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(NovelListModel model)
        {
        }
    }
}
