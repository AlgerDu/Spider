using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Config
{
    /// <summary>
    /// json 文件的配置
    /// </summary>
    public class JsonConfig : IConfig
    {
        public T GetItem<T>(string name = null) where T : IConfigItem, new()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
