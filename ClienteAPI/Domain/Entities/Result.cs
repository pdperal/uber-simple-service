using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        public Result(bool success, string message = default, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
