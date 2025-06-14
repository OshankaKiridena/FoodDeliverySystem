using System.Linq;
using System.Windows;

namespace FoodDeliveryApp
{
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            //xt = $"Total: Rs {RestaurantWindow.Cart.Sum(x => x.Total):0.00}";
        }
                                                     //Abstraction:hides payment details inside PaymentGatewayWindow
        private void PayNow_Click(object sender, RoutedEventArgs e)
        {
            PaymentGatewayWindow gateway = new PaymentGatewayWindow();
            gateway.ShowDialog();
            this.Close();
        }
    }
}
