using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Windows;

namespace FoodDeliveryApp
{
    public partial class SignupPage : Window
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string customerAddress = txtAddress.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(customerAddress) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "⚠ Please fill in all fields.";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "❌ Passwords do not match.";
                return;
            }

            string hashedPassword = PasswordHasher.HashPassword(password); // 🔐 Hash after confirming

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                string insertQuery = "INSERT INTO Users (FullName, Username, CustomerAddress, Password) " +
                                     "VALUES (@name, @user, @address, @pass)";

                using SqlCommand cmd = new(insertQuery, conn);
                cmd.Parameters.AddWithValue("@name", fullName);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@address", customerAddress);
                cmd.Parameters.AddWithValue("@pass", hashedPassword);

                cmd.ExecuteNonQuery();

                MessageBox.Show("🎉 Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation
                    lblMessage.Text = "⚠ Username already exists!";
                else
                    lblMessage.Text = $"Database error: {ex.Message}";
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
