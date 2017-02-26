using System;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 小说列表信息
    /// </summary>
    public class NovelModel
    {
        /// <summary>
        /// 小说 GUID
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// 小说名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
    }
}