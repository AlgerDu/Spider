using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class PluginSymbol : IPluginSymbol
    {
        public PluginType? PType => throw new NotImplementedException();

        public Guid PluginTypeUid => throw new NotImplementedException();

        public int? InstanceID => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public bool Equals(IPluginSymbol x, IPluginSymbol y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IPluginSymbol obj)
        {
            throw new NotImplementedException();
        }
    }
}
