using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliverySystem
{
    public class Order
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }

        public Order()
        {
            OrderTime = DateTime.Now;
            Status = "Pending";
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.TotalPrice;
            }
            return total;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Order placed at: {OrderTime}");
            sb.AppendLine("Items:");
            foreach (var item in Items)
            {
                sb.AppendLine($"- {item}");
            }
            sb.AppendLine($"Total: ${GetTotalPrice():F2}");
            sb.AppendLine($"Status: {Status}");
            return sb.ToString();
        }
    }
}
