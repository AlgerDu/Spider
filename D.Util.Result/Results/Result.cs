using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Results
{
    public class Result : IResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public static IResult<T> Success<T>(T data = default(T))
        {
            return new Result<T>()
            {
                Code = 0,
                Data = data
            };
        }

        public static IResult<T> Error<T>(string msg = null, T data = default(T))
        {
            return new Result<T>()
            {
                Code = 1,
                Message = msg,
                Data = data
            };
        }

        public static IResult Success()
        {
            return new Result()
            {
                Code = 0,
                Message = null
            };
        }

        public static IResult Error(string msg = null)
        {
            return new Result()
            {
                Code = 1,
                Message = msg
            };
        }
    }

    public class Result<T> : IResult<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
