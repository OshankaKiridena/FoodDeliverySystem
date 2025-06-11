using System.Windows;

namespace FoodDeliveryApp
{
    public partial class PaymentGatewayWindow : Window
    {
        public PaymentGatewayWindow()
        {
            InitializeComponent();
        }

        private void ConfirmPayment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Payment successful. Order placed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //RestaurantWindow.Cart.Clear(); // Call Clear() by itself, no assignment!
            this.Close();
        }

    }
}
