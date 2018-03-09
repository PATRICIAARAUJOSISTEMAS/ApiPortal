using Domain.Resources;
using System.Text;

namespace Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public User()
        {
        }

        public User(string Id, string nickName, string password)
        {
            SetId(Id);
            SetNickName(nickName);
            SetPassword(password);
            SetRegistration();
        }

        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string NickName { get; private set; }

        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "Email"));
            Email = ToUpper(email);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "FirstName"));
            FirstName = ToUpper(firstName);
        }

        public void SetFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "FullName"));
            FullName = ToUpper(fullName);
        }

        public void SetNickName(string nickName)
        {
            if (string.IsNullOrEmpty(nickName))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "NickName"));
            NickName = ToUpper(nickName);
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, "Password"));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}