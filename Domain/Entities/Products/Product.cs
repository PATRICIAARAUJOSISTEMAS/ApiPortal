using Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public Product(string name, decimal? price, string id)
        {
            SetId(id);
            SetName(name);
            SetPrice(price);
            SetRegistration();
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public void SetDescription(string description) => Description = ToUpper(description);

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_VAZIO, Message.O, Message.Nome));

            Name = ToUpper(name);
        }

        public void SetPrice(decimal? price)
        {
            if (!price.HasValue)
                AddMessage(string.Format(Message.X0_X1_NAO_PODE_SER_NULO, Message.O, Message.Valor));

            Price = price.Value;
        }
    }
}