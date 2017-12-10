using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.AssociationMap
{
    /// <summary>
    /// 关联节点
    /// </summary>
    internal class AssociationNode<A, B>
    {
        public A ObjectA { get; set; }

        public B ObjectB { get; set; }

        public AssociationNode<A, B> Next { get; set; }
    }
}
