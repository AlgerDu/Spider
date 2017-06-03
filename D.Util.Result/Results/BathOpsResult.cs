using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Results
{
    public class BathOpsResultItem : IBathOpsResultItem
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// item 在批量操作中顺序
        /// </summary>
        public long Index { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
    }

    public class BathOpsResult : IBathOpsResult
    {
        Dictionary<long, IBathOpsResultItem> _items = new Dictionary<long, IBathOpsResultItem>();

        public long OpsCount { get; private set; }

        public int Code
        {
            get
            {
                var count = _items.Values.Count(i => i.Code == 0);
                if (count == 0)
                {
                    return 0;
                }
                else if (count < OpsCount)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }

        public string Message { get; set; }

        public IEnumerable<IBathOpsResultItem> Items
        {
            get
            {
                return _items.Values.OrderBy(i => i.Index);
            }
        }

        public BathOpsResult(long opsCount)
        {
            OpsCount = opsCount;
        }

        /// <summary>
        /// 添加成功项
        /// </summary>
        /// <param name="item"></param>
        public void AddSuccessItem(long index, string msg = null)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem();
            item.Code = 0;
            item.Index = index;
            item.Message = msg;

            _items.Add(index, item);
        }

        /// <summary>
        /// 添加失败项
        /// </summary>
        /// <param name="item"></param>
        public void AddErrorItem(long index, string msg = null)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem();
            item.Code = 1;
            item.Index = index;
            item.Message = msg;

            _items.Add(index, item);
        }

        public void AddResultItem(long index, IResult result)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem();
            item.Code = result.Code;
            item.Index = index;
            item.Message = result.Message;

            _items.Add(index, item);
        }
    }

    public class BathOpsResultItem<T> : IBathOpsResultItem<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// item 在批量操作中顺序
        /// </summary>
        public long Index { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 需要返回的数据
        /// </summary>
        public T Data { get; set; }
    }

    public class BathOpsResult<T> : IBathOpsResult<T>
        where T : class
    {
        Dictionary<long, IBathOpsResultItem<T>> _items = new Dictionary<long, IBathOpsResultItem<T>>();

        public long OpsCount { get; private set; }

        public int Code
        {
            get
            {
                var count = _items.Values.Count(i => i.Code == 0);
                if (count == 0)
                {
                    return 0;
                }
                else if (count < OpsCount)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }

        public string Message { get; set; }

        public IEnumerable<IBathOpsResultItem<T>> Items
        {
            get
            {
                return _items.Values.OrderBy(i => i.Index);
            }
        }

        public BathOpsResult(long opsCount)
        {
            OpsCount = opsCount;
        }

        /// <summary>
        /// 添加成功项
        /// </summary>
        /// <param name="item"></param>
        public void AddSuccessItem(long index, T data = null, string msg = null)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem<T>();
            item.Code = 0;
            item.Index = index;
            item.Message = msg;
            item.Data = data;

            _items.Add(index, item);
        }

        /// <summary>
        /// 添加失败项
        /// </summary>
        /// <param name="item"></param>
        public void AddErrorItem(long index, string msg = null, T data = null)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem<T>();
            item.Code = 1;
            item.Index = index;
            item.Message = msg;
            item.Data = data;

            _items.Add(index, item);
        }

        public void AddResultItem(long index, IResult<T> result)
        {
            if (_items.ContainsKey(index))
            {
                throw new Exception("MyZone.Server.Models.DTO.BathOpsResult 添加重复的 item，编号：" + index);
            }

            var item = new BathOpsResultItem<T>();
            item.Code = result.Code;
            item.Index = index;
            item.Message = result.Message;
            item.Data = result.Data;

            _items.Add(index, item);
        }
    }
}
