using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodDeliveryApp
{
    //<summary>
    //Interaction logic for RestaurantWindow.xaml
    //</summary>
    public partial class RestaurantWindow : Window
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        private readonly List<Restaurant> restaurants = new();
        public RestaurantWindow()
        {
            InitializeComponent();
            LoadRestaurants();

        }
        private void LoadRestaurants()
        {
            restaurants.Clear();

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = @"SELECT Id, RestaurantName, RestaurantDetails FROM Restaurant  ";

            using SqlCommand cmd = new(query, conn);
          

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                restaurants.Add(new Restaurant
                {
                    Id = reader.GetInt32(0),
                   Name = reader.GetString(1),
                 Details = reader.GetString(2),
                   
                });
            }

            dataGridMenu.ItemsSource = null;
            dataGridMenu.ItemsSource = restaurants;


        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow dashboard = new CartWindow();
            dashboard.Show();
            // Logic to add selected item to cart
            // This is just a placeholder; actual implementation will depend on your application logic
            MessageBox.Show("Item added to cart!");
        }

        private void lvRestaurants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            Restaurant restaurant = dataGridMenu.SelectedItem as Restaurant;
            MenuItemWindow dashboard = new MenuItemWindow(restaurant.Id);
            dashboard.Show();
        }
    }



    // Assuming a CartItem class exists or needs to be defined
    public class CartItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }




}


