using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.AssociationMap
{
    /// <summary>
    /// 有关 A 的 Enumerator
    /// </summary>
    internal class AEnumerator<A, B> : IEnumerator<A>
    {
        AssociationNode<A, B> _head;
        AssociationNode<A, B> _current;

        public A Current => _current.ObjectA;

        object IEnumerator.Current => _current.ObjectA;

        public AEnumerator(AssociationNode<A, B> head)
        {
            _head = head;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_current.NextA != null)
            {
                _current = _current.NextA;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _current = _head;
        }
    }
}
