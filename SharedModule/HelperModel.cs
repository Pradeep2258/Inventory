using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModule
{
    public class ResponseModel<T>
    {
        public T ResponseBody { get; set; }
        public Status ResponseStatus { get; set; }
    }

    public class Status
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class JwtUserDetails
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public int UserRole { get; set; }
    }
}