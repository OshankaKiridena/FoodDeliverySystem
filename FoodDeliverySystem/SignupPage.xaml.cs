using System;
using System.Windows;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace FoodDeliveryApp
{
    public partial class SignupPage : Window
    {
        public SignupPage()
        {
            InitializeComponent(); // ✅ DO NOT remove or override this method
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string customeraddress = txtAddress.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Users (FullName, Username,Customeraddress,Password) VALUES (@name, @user,@address,@pass)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", fullName);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@address",customeraddress);
                        cmd.Parameters.AddWithValue("@pass", password); // Consider hashing for security

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Unique constraint violation
                        lblMessage.Text = "Username already exists!";
                    else
                        lblMessage.Text = "Database error: " + ex.Message;
                }
            }
        }
    }
}
