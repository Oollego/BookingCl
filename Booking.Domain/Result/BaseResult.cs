﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Result
{
    public class BaseResult
    {
        public bool IsSuccess => ErrorMessage == null;
        public string ErrorMessage { get; set; } = null!;
        public int? ErrorCode { get; set; }
    }

    public class BaseResult<T> : BaseResult
    {
        public T? Data { get; set; } 

        public BaseResult() { } 
        public BaseResult(string errorMessage, int errorCode, T data) 
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Data = data;
        }
    }
}

