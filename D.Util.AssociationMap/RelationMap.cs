using D.Util.AssociationMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util
{
    /// <summary>
    /// TODO：后面会迁移到 D.Util 项目中；
    /// 一直以来（在公司项目中需要用到多对多的关系存储）都觉得应该实现一个存储多对多的结构；
    /// 感觉好像有一种数据结构是处理这个东西的，但是想不到是什么了，也可能是记错了。
    /// 存储 A 和 B 的多对多关系
    /// </summary>
    public class AssociationMap<A, B>
    {
        Dictionary<A, AHead<A, B>> _dictionaryA;
        
    }
}
