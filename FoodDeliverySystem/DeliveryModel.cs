using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliverySystem.Models
{
    public class DeliveryModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; } // e.g., Pending, Dispatched, Delivered
        public string AssignedTo { get; set; } // Delivery staff name
        public List<string> DeliveryStaffList { get; set; } // Available delivery staff
    }
}

