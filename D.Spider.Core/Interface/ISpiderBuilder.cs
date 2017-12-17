using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISpiderBuilder
    {
        /// <summary>
        /// 使用 Startup 类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISpiderBuilder UseStartup<T>() where T : ISpiderBuilder, new();

        /// <summary>
        /// 创建一个 Spider
        /// </summary>
        /// <returns></returns>
        ISpider Build();
    }
}
