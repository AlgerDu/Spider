using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Config
{
    /// <summary>
    /// json 配置
    /// </summary>
    public class JsonConfig : IConfig
    {
        Dictionary<string, IConfigItem> _items;

        public string Version => throw new NotImplementedException();

        public string Describe => throw new NotImplementedException();

        public T GetItem<T>(string name = null) where T : IConfigItem, new()
        {
            throw new NotImplementedException();
        }

        public void Save(string version = "1.0", string describe = null)
        {
            throw new NotImplementedException();
        }
    }
}
