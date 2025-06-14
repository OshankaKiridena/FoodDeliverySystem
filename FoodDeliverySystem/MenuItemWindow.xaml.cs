using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using System;
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
    /// <summary>
    /// Interaction logic for MenuItemWindow.xaml
    /// </summary>
    public partial class MenuItemWindow : Window
    {

        private int restaurantId;
        
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        private readonly List<MenuItemModel> menuItems = new();
        public MenuItemWindow(int id)
        {
            InitializeComponent();
            restaurantId = id;
            LoadMenuItems();
        }
        private void LoadMenuItems()
        {
            menuItems.Clear();

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = @"SELECT MenuItemId, ItemName, Price, Description FROM MenuItems WHERE RestaurantId = @RestaurantId";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menuItems.Add(new MenuItemModel
                {
                    MenuItemId = reader.GetInt32(0),
                    ItemName = reader.GetString(1),
                    Price = reader.GetInt32(2),
                    Description = reader.GetString(3)
                });
            }

            lvMenuItems.ItemsSource = null;
            lvMenuItems.ItemsSource = menuItems;


        }


        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
           CartWindow page = new CartWindow();
            page.ShowDialog();
        }
    }
}
