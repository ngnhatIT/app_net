using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiapp.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj, string message)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Message = message;
        }
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}