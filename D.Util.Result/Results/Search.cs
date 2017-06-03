using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Results
{
    public class SearchFilter : ISearchFilter
    {
        public string Field { get; set; }

        public string Value { get; set; }

        public bool Order { get; set; }

        public string Op { get; set; }
    }

    public class SerachCondition : ISerachCondition
    {
        public long PageSize { get; set; }

        public long PageIndex { get; set; }

        public IEnumerable<ISearchFilter> FilterItems { get; set; }

        public long SkipCount
        {
            get
            {
                return PageSize * (PageIndex - 1);
            }
        }

        public SerachCondition()
        {
            FilterItems = new List<SearchFilter>();
        }
    }

    public class SearchResult<T> : ISearchResult<T>
        where T : class
    {
        IEnumerable<T> _data;

        public long PageSize { get; set; }

        public long PageIndex { get; set; }

        public long RecodCount { get; private set; }

        public IEnumerable<T> Data
        {
            get
            {
                return _data;
            }
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public SearchResult(long recoderCount)
        {
            RecodCount = recoderCount;
            _data = new List<T>();
        }

        public SearchResult(long total, long index, long size, IEnumerable<T> records)
        {
            RecodCount = total;
            PageIndex = index;
            PageSize = size;
            _data = records;
        }

        public void AddRecord(T record)
        {
            //_data.Add(record);
        }

        public void SetRecords(IEnumerable<T> records)
        {
            _data = records;
        }
    }
}
