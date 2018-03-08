using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Request
{
    public class LoginRequest
    {
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}