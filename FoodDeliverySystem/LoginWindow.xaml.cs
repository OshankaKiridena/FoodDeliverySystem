using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Threading;

namespace FoodDeliveryApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Hide any previous error
            ErrorPanel.Visibility = Visibility.Collapsed;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            // Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Please enter both username and password.");
                return;
            }

            try
            {
                // Use connection string from App.config
                string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                    // Alternative query for future use:
                    // string query = "SELECT id, isstaff FROM Users WHERE Username = @username AND Password = @password";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        // Successful login
                        DashboardWindow dashboard = new DashboardWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        ShowError("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Database connection error: {ex.Message}");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SignupPage page = new SignupPage();
                page.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening signup page: {ex.Message}", "Error",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Forgot password functionality will be implemented soon.",
                           "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);

            // TODO: Implement forgot password functionality
            // ForgotPasswordWindow forgotWindow = new ForgotPasswordWindow();
            // forgotWindow.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            ErrorPanel.Visibility = Visibility.Visible;

            // Auto-hide error after 5 seconds
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += (s, e) => {
                ErrorPanel.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }
    }

    // Note: Move this to a separate file if it's a full window implementation
    public partial class DisplayRestaurants : Window
    {
        // TODO: Implement DisplayRestaurants functionality
        // This should probably be in its own separate file: DisplayRestaurants.xaml.cs
    }
}