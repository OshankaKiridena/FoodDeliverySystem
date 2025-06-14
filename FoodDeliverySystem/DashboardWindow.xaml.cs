
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

        // Navigate to RestaurantWindow
        private void Restaurants_Click(object sender, RoutedEventArgs e)
        {
            RestaurantWindow restaurantWindow = new RestaurantWindow();
            restaurantWindow.Show();
            this.Close();
        }

        // Placeholder for Deliveries button (currently disabled in UI)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("🚚 Deliveries feature is coming soon!", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle Close button (✕) click
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

