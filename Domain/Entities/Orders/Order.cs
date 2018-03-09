using Domain.Resources;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string id, string user)
        {
            SetId(id);
            SetUser(user);
        }

        public string UserId { get; set; }

        public void SetUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                AddMessage(string.Format(Message.X0_X1_NAO_ENCONTRADO, Message.O, Message.Usuario));

            UserId = userId;
        }
    }
}