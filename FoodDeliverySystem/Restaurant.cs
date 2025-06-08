using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliverySystem
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public bool Validate() 
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address);
        }
    }
}
