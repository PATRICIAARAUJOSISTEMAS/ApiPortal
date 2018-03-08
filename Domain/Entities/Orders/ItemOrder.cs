using Domain.Entities.Products;
using Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Orders
{
    public class ItemOrder : BaseEntity
    {
        public ItemOrder(string orderId, string product, decimal? quantity)
        {
            SetOrderId(orderId);
            SetProduct(product);
            SetQuantity(quantity);
        }

        public string OrderId { get; private set; }

        public string ProductId { get; private set; }

        public decimal Quantity { get; private set; }

        public void SetOrderId(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                AddMessage(Message.X0_X1_NAO_PODE_SER_NULO);

            OrderId = orderId;
        }

        public void SetProduct(string product)
        {
            if (string.IsNullOrEmpty(product))
                AddMessage(Message.X0_X1_NAO_PODE_SER_NULO);

            ProductId = product;
        }

        public void SetQuantity(decimal? quantity)
        {
            if (!quantity.HasValue && quantity == 0)
                AddMessage(Message.X0_X1_NAO_PODE_SER_NULO);

            Quantity = quantity.Value;
        }
    }
}