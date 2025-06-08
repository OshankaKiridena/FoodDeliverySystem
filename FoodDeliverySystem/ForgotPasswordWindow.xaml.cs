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
                lblStatus.Text = "Please enter your email.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // TODO: Connect to database to check email and reset password logic

            lblStatus.Text = "If the email is registered, a reset link has been sent.";
            lblStatus.Foreground = System.Windows.Media.Brushes.Green;
        }
    }
}
