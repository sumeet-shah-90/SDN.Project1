using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1.API.ViewModels
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}