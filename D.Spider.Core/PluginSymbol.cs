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
        public PluginType? PType { get; set; }

        public int? InstanceID { get; set; }

        public string Name { get; set; }

        public bool Equals(IPluginSymbol x, IPluginSymbol y)
        {
            if (x.PType.HasValue && y.PType.HasValue && x.PType != y.PType)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(y.Name) && x.Name != y.Name)
            {
                return false;
            }

            if (x.InstanceID.HasValue && y.InstanceID.HasValue && x.InstanceID != y.InstanceID)
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(IPluginSymbol obj)
        {
            return obj.InstanceID.GetHashCode() * obj.PType.GetHashCode() * obj.PType.GetHashCode();
        }

        public override string ToString()
        {
            return $"{InstanceID}({Name})";
        }

        public static bool operator ==(PluginSymbol lhs, PluginSymbol rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PluginSymbol lhs, PluginSymbol rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
