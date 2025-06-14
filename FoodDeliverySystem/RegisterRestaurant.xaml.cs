using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Windows;

namespace FoodDeliveryApp
{
    public partial class RegisterRestaurant : Window
    {
        public RegisterRestaurant()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string restaurantName = txtRestaurantName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(restaurantName) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Please fill all required fields.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // (Optional) Password hashing could go here
            // string hashedPassword = PasswordHasher.HashPassword(password);

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                string insertQuery = @"INSERT INTO Restaurant (RestaurantName, Description, Username, Password)
                                       VALUES (@name, @description, @user, @pass)";

                using SqlCommand cmd = new(insertQuery, conn);
                cmd.Parameters.AddWithValue("@name", restaurantName);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password); // Replace with hashedPassword for security

                cmd.ExecuteNonQuery();

                MessageBox.Show("🎉 Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation
                    lblMessage.Text = "❌ Username already exists!";
                else
                    lblMessage.Text = "Database error: " + ex.Message;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
