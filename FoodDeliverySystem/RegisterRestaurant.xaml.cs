using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FoodDeliveryApp
{
    public partial class RegisterRestaurant : Window
    {
        public static List<RestaurantProfileWindow> RegisteredRestaurants = new List<RestaurantProfileWindow>();



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
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Restaurant (restaurantName,Description, Username, Password) VALUES (@name,@description, @user, @pass)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", restaurantName);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@user", username);
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


                RestaurantProfileWindow restaurant = new RestaurantProfileWindow
                {
                    Name = restaurantName,
                    Description = description,
                    Username = username,
                    Password = password,
                    //MenuItems = new List<MenuItem>()
                };

                // RegisterRestaurant.Add(restaurant);

                lblStatus.Text = "Restaurant profile registered successfully!";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;

                RestaurantProfileWindow profileWindow = new RestaurantProfileWindow();
                // profileWindow.Show();
                this.Close();
            }
        }

        public class RestaurantProfileWindow
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }




        }
    }
}
    

   
