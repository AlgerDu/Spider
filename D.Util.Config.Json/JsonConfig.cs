using D.Util.Interface;
using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// path 的分隔字符串
        /// </summary>
        const char _splitChar = '.';

        const string _version = "version";
        const string _describe = "describe";

        /// <summary>
        /// json 文件中读取到的内容对象
        /// </summary>
        JObject _fileContent;

        /// <summary>
        /// 所有的配置项
        /// save 需要做的事情就是把 items 中的内容全部保存到 fileContent 然后保存到文件
        /// </summary>
        Dictionary<string, IConfigItem> _items;

        public string Version
        {
            get
            {
                return JsonValue(_version).ToString();
            }
        }

        public string Describe
        {
            get
            {
                return JsonValue(_describe).ToString();
            }
        }

        public T GetItem<T>(string name = null) where T : class, IConfigItem, new()
        {
            var item = new T();

            var path = name == null ? item.Path : item.Path + _splitChar + name;

            if (_items.ContainsKey(path))
            {
                item = _items[path] as T;
            }
            else
            {
                var jsonItem = JsonValue(path);

                if (jsonItem != null)
                {
                    item = jsonItem.ToObject<T>();
                }
                _items.Add(path, item);
            }

            return item;
        }

        public void Save(string version = "1.0", string describe = null)
        {
            throw new NotImplementedException();
        }

        public void LoadFile(string path)
        {

        }

        /// <summary>
        /// 获取 file content 中值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private JToken JsonValue(string path)
        {
            JToken value = _fileContent;

            var nameArray = path.Split(_splitChar);

            foreach (var name in nameArray)
            {
                value = value[name];
            }

            return value;
        }

        /// <summary>
        /// 向 file content 中添加或者保存配置
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        private void JsonValue(string path, object value)
        {

        }
    }
}
