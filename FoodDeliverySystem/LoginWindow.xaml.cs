using FoodDeliverySystem;  // ✅ Make sure this is added
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter both username and password.";
                return;
            }

            // ✅ Use connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                //string query = "SELECT id, isstaff FROM Users WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                

                int count = (int)cmd.ExecuteScalar();

                if (count == 1)
                {
                    
                    
                    
                    DashboardWindow dashboard = new DashboardWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    lblError.Text = "Invalid username or password.";
                }
            }
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignupPage page = new SignupPage();
            page.ShowDialog();

        }



        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }
        public partial class DisplayRestaurants : Window
        {
           
        }




    }
}

