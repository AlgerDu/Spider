using D.Util.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        #region const 变量

        /// <summary>
        /// path 的分隔字符串
        /// </summary>
        const char _splitChar = '.';

        const string _version = "version";
        const string _describe = "describe";
        #endregion

        #region 字段
        /// <summary>
        /// json 文件中读取到的内容对象
        /// </summary>
        JObject _fileContent;

        /// <summary>
        /// 所有的配置项
        /// save 需要做的事情就是把 items 中的内容全部保存到 fileContent 然后保存到文件
        /// </summary>
        Dictionary<string, IConfigItem> _items;

        /// <summary>
        /// json 文件的路径
        /// </summary>
        string _filePath;
        #endregion

        #region IConfig 属性
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

        /// <summary>
        /// 配置文件的路径
        /// </summary>
        public string Path
        {
            get
            {
                return _filePath;
            }
        }
        #endregion

        #region IConfig 方法
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
                    try
                    {
                        item = jsonItem.ToObject<T>();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("配置文件读取 " + new T().Path + " 配置项错误：", ex);
                    }
                }
                _items.Add(path, item);
            }

            return item;
        }

        public void Save(string version = "1.0", string describe = null)
        {
            JsonValue(_version, version);
            JsonValue(_describe, describe);

            foreach (var path in _items.Keys)
            {
                JsonValue(path, _items[path]);
            }

            SaveJsonToFile();
        }

        public void LoadFile(string path)
        {
            _filePath = path;

            if (!File.Exists(path))
            {
                _fileContent = new JObject();
                return;
                //throw new Exception(path + " 配置文件不存在");
            }

            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    JsonReader jr = new JsonTextReader(sr);

                    _fileContent = JObject.Load(jr);
                }
                catch (Exception ex)
                {
                    throw new Exception("读取配置文件 " + path + " 失败：" + ex.ToString());
                }
            }
        }
        #endregion

        public JsonConfig()
        {
            _items = new Dictionary<string, IConfigItem>();
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

                if (value == null)
                {
                    return value;
                }
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
            if (value == null)
            {
                return;
            }

            JObject content = _fileContent;

            var nameArray = path.Split(_splitChar);

            var name = nameArray[0];

            for (var i = 0; i < nameArray.Length - 1; i++)
            {
                if (content.Property(name) == null)
                {
                    content.Add(name, new JObject());
                }

                content = content[name] as JObject;

                name = nameArray[i + 1];
            }

            if (value.GetType() == typeof(string))
            {
                content.Remove(name);
                content.Add(name, value as string);
            }
            else
            {
                var js = new JsonSerializer();
                js.ContractResolver = new WritablePropertiesOnlyResolver();

                content[name] = JObject.FromObject(value, js);
            }
        }

        /// <summary>
        /// 将 file content 保存到文件中
        /// </summary>
        private void SaveJsonToFile()
        {
            using (StreamWriter wr = new StreamWriter(_filePath))
            {
                try
                {
                    JsonWriter jr = new JsonTextWriter(wr);
                    jr.Formatting = Formatting.Indented;

                    _fileContent.WriteTo(jr);

                    jr.Close();
                    wr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("保存配置到 json 文件失败：" + ex.ToString());
                }
            }
        }
    }
}
