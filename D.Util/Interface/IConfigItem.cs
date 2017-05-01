using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 配置文件中不同模块的配置项
    /// 请不要使用 new 的方式产生一个 item，请使用 IConfig.GetItem 方法
    /// </summary>
    public interface IConfigItem
    {
        string Path { get; }
    }
}
