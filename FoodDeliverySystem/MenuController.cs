using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliverySystem
{
    public class MenuController
    {
        private List<MenuItem> menuItems = new List<MenuItem>();

        public void AddMenuItem(MenuItem item) => menuItems.Add(item);
        public List<MenuItem> GetMenuItems() => menuItems;
        public void UpdateMenuItem(MenuItem updatedItem)
        {
            var item = menuItems.FirstOrDefault(m => m.Id == updatedItem.Id);
            if (item != null)
            {
                item.Name = updatedItem.Name;
                item.Description = updatedItem.Description;
                item.Price = updatedItem.Price;
                item.Category = updatedItem.Category;
            }
        }
        public void DeleteMenuItem(int id) =>
            menuItems.RemoveAll(m => m.Id == id);
    }

}
