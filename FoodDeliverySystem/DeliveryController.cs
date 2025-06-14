using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class DeliveryController

    {
        //Encapsulation
        private List<string> deliveryStaffList = new() { "Ravi", "Amal", "Nimesh" };

        public void AssignDelivery(DeliveryModel delivery, string assignedTo)
        {
            if (string.IsNullOrEmpty(assignedTo) || !deliveryStaffList.Contains(assignedTo))
            {
                throw new ArgumentException("Invalid delivery person.");
            }
            delivery.AssignedTo = assignedTo;
        }

        public void UpdateDeliveryStatus(DeliveryModel delivery, string newStatus)
        {
            string[] validStatuses = { "Pending", "Dispatched", "In Transit", "Delivered" };
            if (string.IsNullOrEmpty(newStatus) || !Array.Exists(validStatuses, s => s == newStatus))
            {
                throw new ArgumentException("Invalid status. Use Pending, Dispatched, In Transit, or Delivered.");
            }
            delivery.Status = newStatus;
        }
    }
}