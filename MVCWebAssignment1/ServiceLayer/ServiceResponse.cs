using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class ServiceResponse
    {
        public bool Result { get; set; }
        public Object ServiceObject { get; set; }
        public int ReturnInt { get; set; }
    }
}