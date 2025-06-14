using System;
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
            string cardNumber = txtCard.Text.Trim();
            string holderName = txtHolder.Text.Trim();
            string expiry = txtExpiry.Text.Trim();
            string cvv = txtCVV.Password.Trim();

            // Basic validation
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 12 ||
                string.IsNullOrEmpty(holderName) ||
                string.IsNullOrEmpty(expiry) ||
                string.IsNullOrEmpty(cvv) || cvv.Length < 3)
            {
                MessageBox.Show("⚠ Please fill all payment fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Optionally: Validate expiry format with regex/MM/YY logic here

            // Simulated success
            MessageBox.Show("✅ Payment successful. Your order has been placed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example: Clear cart if needed
            // RestaurantWindow.Cart.Clear();

            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
