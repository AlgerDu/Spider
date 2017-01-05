using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// 如何处理爬取好的页面
    /// </summary>
    public class PageProcessStep
    {
        /// <summary>
        /// 处理方式
        /// </summary>
        /// <returns></returns>
        public PageProcessStepTypes Type { get; set; }

        /// <summary>
        /// 这一步获取到的是不是数组
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// 处理字符串
        /// </summary>
        /// <returns></returns>
        public string ProcessStr { get; set; }

        /// <summary>
        /// 在这一步可以获取到的数据的名称
        /// 以 “;” 分割
        /// </summary>
        /// <returns></returns>
        public string DataNames { get; set; }

        /// <summary>
        /// 下一步的处理方式
        /// </summary>
        /// <returns></returns>
        public PageProcessStep NextProcessStep { get; set; }
    }
}
