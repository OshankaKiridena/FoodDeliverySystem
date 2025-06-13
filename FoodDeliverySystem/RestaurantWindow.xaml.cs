
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace FoodDeliveryApp
{
    public partial class RestaurantWindow : Window
    {
        public static List<CartItem> Cart { get; set; } = new List<CartItem>();

        public RestaurantWindow()
        {
            InitializeComponent(); // THIS loads the XAML UI!
        }
    }

    public class CartItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
    





