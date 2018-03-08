using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Requests
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }

        public DateTime? Registration { get; set; }
    }
}