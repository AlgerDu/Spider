using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    /// <summary>
    /// http 返回结果
    /// </summary>
    public class Result : IResult
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class Result<T> : Result, IResult<T>
        where T : class
    {
        public T Data { get; set; }
    }
}
