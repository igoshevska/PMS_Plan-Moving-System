﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels
{
    public class ApiResponseModel<T>
    {
        public ErrorHandler errorHandler { get; set; }
        public T response { get; set; }
    }

    public class ErrorHandler 
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }



}
