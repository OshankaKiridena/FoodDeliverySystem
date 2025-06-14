using System.Windows;

namespace FoodDeliveryApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Open Login window and close Main
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        // Open Restaurants window
        // private void Restaurants_Click(object sender, RoutedEventArgs e)
        // {
        //     RestaurantWindow restaurantWindow = new RestaurantWindow();
        //     restaurantWindow.ShowDialog();
        // }

        // Exit app
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StaffLogin_Click(object sender, RoutedEventArgs e)
        {
            RestaurantLogin staffLogin = new RestaurantLogin();
            staffLogin.Show();
            this.Close();
        }
        
    }
}