using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 配置文件读取和保存
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 配置文件的版本
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 配置文件的描述
        /// </summary>
        string Describe { get; }

        /// <summary>
        /// 获取某个模块的配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetItem<T>(string name = null) where T : IConfigItem, new();

        /// <summary>
        /// 将持有的配置项保存到文件
        /// </summary>
        /// <param name="version">版本号</param>
        /// <param name="describe">配置描述</param>
        void Save(string version = "1.0", string describe = null);
    }
}
