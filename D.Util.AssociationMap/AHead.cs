using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.AssociationMap
{
    /// <summary>
    /// A 的头节点
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    internal class AHead<A, B> : IEnumerable<B>
    {
        Dictionary<A, AssociationNode<A, B>> _dictionaryA;



        public IEnumerator<B> GetEnumerator()
        {
            var bs = _dictionaryA.Values.Select(nn => nn.ObjectB);
            return bs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
