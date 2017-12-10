using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// TODO：后面会迁移到 D.Util 项目中；
    /// 存储 A 和 B 的多对多关系（线程安全）；
    /// A 和 B 的地位是有差异的
    /// 一直以来（在公司项目中需要用到多对多的关系存储）都觉得应该实现一个存储多对多的结构；
    /// 感觉好像有一种数据结构是处理这个东西的，但是想不到是什么了，也可能是记错了。
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    public interface IAssociationMap<A, B>
    {
        /// <summary>
        /// 获取和 B 实例 ob 相关联的所有 A 实例
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        IEnumerable<A> Associations(B b);

        /// <summary>
        /// 获取和 A 实例 oa 相关联的所有 B 实例
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        IEnumerable<B> Associations(A a);

        /// <summary>
        /// 添加 a 和 b 的关联关系
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void Associate(A a, B b);

        /// <summary>
        /// 取消 a 和 b 之间的关系
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        void RemoveAssociation(A a, B b);

        /// <summary>
        /// a 和 b 是否有关联关系
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        bool HasAssociation(A a, B b);
    }
}
