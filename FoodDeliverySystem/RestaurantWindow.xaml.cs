
using System;
using System.Collections.Generic;
using System.Windows;

namespace FoodDeliveryApp
{
    public partial class RestaurantWindow : Window
    {
        public static List<CartItem> Cart { get; set; } = new List<CartItem>();

        public RestaurantWindow()
        {
            InitializeComponent(); // THIS loads the XAML UI!
        }
    }

    public class CartItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
    

        private void LoadRestaurants()
        {
            restaurantList = new List<Restaurant>
            {
                new Restaurant { Name = "The Food Hub", Description = "Multi-cuisine restaurant" },
                new Restaurant { Name = "Green Eatery", Description = "Healthy food options" },
                new Restaurant { Name = "Spicy Treats", Description = "South Asian flavors" },
                new Restaurant { Name = "Ocean Grill", Description = "Seafood special" }
            };

            lvRestaurants.ItemsSource = restaurantList;
            txtTotalCount.Text = restaurantList.Count.ToString();
            txtActiveCount.Text = "3"; // Set dynamically if you have "Status" property
        }

        // ✅ Handles Close button click
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ✅ Refreshes restaurant list
        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            LoadRestaurants();
            MessageBox.Show("Restaurant list refreshed.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ✅ Shows details of selected restaurant
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvRestaurants.SelectedItem is Restaurant selected)
            {
                MessageBox.Show($"📋 {selected.Name}\n\n{selected.Description}", "Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a restaurant to view details.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
