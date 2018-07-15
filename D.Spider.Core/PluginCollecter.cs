using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class PluginCollecter : IPluginCollecter
    {
        Dictionary<string, Type> _dic_PluginTypes;

        public PluginCollecter()
        {
            _dic_PluginTypes = new Dictionary<string, Type>();
        }

        public bool Collect<T>() where T : IPlugin
        {
            return Collect(typeof(T));
        }

        public bool Collect(Type plugingType)
        {
            var fullName = plugingType.FullName;

            if (_dic_PluginTypes.ContainsKey(fullName))
            {
                return false;
            }
            else
            {
                _dic_PluginTypes.Add(fullName, plugingType);
                return true;
            }
        }

        public IEnumerable<Type> GetCollectedPluginType()
        {
            return _dic_PluginTypes.Values;
        }
    }
}
