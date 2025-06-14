using FoodDeliverySystem;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Windows;

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
                lblError.Text = "Please enter both username and password.";
                return;
            }

            // Use connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Old logic: get the restaurant id if credentials match
                string query = "SELECT id, restaurantname FROM restaurant WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int restaurantId = reader.GetInt32(0);
                    string restaurantName = reader.GetString(1);

                    // After successful login, open the profile window for that restaurant
                    RestaurantProfileWindow dashboard = new RestaurantProfileWindow(restaurantId,restaurantName );
                    dashboard.Show();
                    this.Close();

                }
                else
                {
                    lblError.Text = "Invalid username or password.";
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterRestaurant registerWindow = new RegisterRestaurant();
            registerWindow.ShowDialog();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotWindow = new ForgotPasswordWindow();
            forgotWindow.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
