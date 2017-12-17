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
        /// <typeparam name="T">自定义 ISpiderBuilder</typeparam>
        /// <returns></returns>
        ISpiderBuilder UseStartup<T>() where T : IStartup, new();

        /// <summary>
        /// 创建一个 Spider
        /// </summary>
        /// <returns></returns>
        ISpider Build();
    }
}
