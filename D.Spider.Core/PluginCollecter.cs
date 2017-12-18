using D.Spider.Core.Interface;
using D.Spider.Core.Interface.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class PluginCollecter : IPluginCollecter
    {
        public void Collect<T>() where T : IPlugin
        {
            throw new NotImplementedException();
        }

        public bool Collect(Type plugingType)
        {
            throw new NotImplementedException();
        }
    }
}
