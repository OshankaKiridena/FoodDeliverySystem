using FoodDeliveryApp;
using System.Windows;

namespace FoodDeliverySystem

{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        

        private void Restaurants_Click(object sender, RoutedEventArgs e)
        {
            RestaurantWindow window = new RestaurantWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
