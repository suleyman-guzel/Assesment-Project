using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities
{
    public class ApiDataResult<T>
    {
        public ApiDataResult()
        {

        }
        public ApiDataResult(string message, T data, bool success, string errorMessage)
        {
            Message = message;
            Data = data;
            Success = success;
            ErrorMessage = errorMessage;
        }
        public ApiDataResult(string message, T data, bool success)
        {
            Message = message;
            Data = data;
            Success = success;            
        }
        public ApiDataResult(bool success,string message)
        {
            Success = success;
            Message = message;
        }
        public ApiDataResult(bool success, string message,string errorMessage)
        {
            Success = success;
            Message = message;
            ErrorMessage = errorMessage;
        }

        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

    }
}
