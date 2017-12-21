using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 可以奔跑的，哈哈哈哈哈哈
    /// </summary>
    public interface IRunningable<T>
    {
        /// <summary>
        /// 是否正在运行
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        T Run();

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        T Stop();
    }
}
