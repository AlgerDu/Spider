using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// Spider 接口
    /// 内部组件通过 event 交互
    /// 内部维护一个依赖注入的容器
    /// </summary>
    public interface ISpider : IRunningable<ISpider>
    {
    }
}
