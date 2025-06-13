using System;

namespace FoodDeliverySystem
{
    public class OrderItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice
        {
            get { return Quantity * UnitPrice; }
        }

        public OrderItem(string itemName, int quantity, decimal unitPrice)
        {
            ItemName = itemName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override string ToString()
        {
            return $"{ItemName} x{Quantity} - ${TotalPrice:F2}";
        }
    }
}
