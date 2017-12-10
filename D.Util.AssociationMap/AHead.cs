using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.AssociationMap
{
    internal class AHead<A, B> : AssociationNode<A, B>, IEnumerable<A>
    {
        public IEnumerator<A> GetEnumerator()
        {
            return new AEnumerator<A, B>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new AEnumerator<A, B>(this);
        }
    }
}
