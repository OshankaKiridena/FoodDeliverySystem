using System.Windows;

namespace FoodDeliveryApp
{
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblStatus.Text = "⚠ Please enter your registered email.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // Simulate sending a password reset link or OTP
            lblStatus.Text = "✅ Password reset instructions have been sent to your email.";
            lblStatus.Foreground = System.Windows.Media.Brushes.Green;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
