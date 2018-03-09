using System;

namespace Domain.Responses
{
    public class UserResponse : ResponseBase
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public string NickName { get; set; }
    }
}