using System.Collections.Generic;

namespace FoodDeliverySystem
{
    public class OrderController
    {
        private List<Order> orders = new List<Order>();
        private int nextOrderId = 1;

        public Order PlaceOrder(Order order)
        {
            // Assign an ID (optional)
            order.Status = "Placed";
            orders.Add(order);
            return order;
        }

        public List<Order> GetAllOrders()
        {
            return orders;
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            return orders.FindAll(o => o.Status == status);
        }

        public void CancelOrder(Order order)
        {
            order.Status = "Cancelled";
        }

        public void MarkOrderAsDelivered(Order order)
        {
            order.Status = "Delivered";
        }
    }
}
