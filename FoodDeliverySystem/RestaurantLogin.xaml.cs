using System;
using System.Configuration;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace FoodDeliveryApp
{
    public partial class RestaurantLogin : Window
    {
        public RestaurantLogin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "⚠ Please enter both username and password.";
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

                using SqlConnection conn = new(connectionString);
                conn.Open();

                string query = "SELECT id FROM restaurant WHERE Username = @username AND Password = @password";
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int restaurantId = reader.GetInt32(0);
                    Application.Current.Properties["RestaurantId"] = restaurantId;

                    RestaurantProfileWindow dashboard = new();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    lblError.Text = "❌ Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging in:\n{ex.Message}", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterRestaurant registerWindow = new();
            registerWindow.ShowDialog();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotWindow = new();
            forgotWindow.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
