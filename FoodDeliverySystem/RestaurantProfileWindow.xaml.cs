using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FoodDeliveryApp
{
    public partial class RestaurantProfileWindow : Window
    {
        private int restaurantId;
        private string restaurantName;
        private string imagePath = "";
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        private readonly List<MenuItemModel> menuItems = new();

        // Constructor to initialize with restaurant id and name
        public RestaurantProfileWindow(int restaurantId, string restaurantName)
        {
            InitializeComponent();
            this.restaurantId = restaurantId;
            this.restaurantName = restaurantName;
            lblRestaurant.Text = restaurantName;
            txtRestaurantName.Text = restaurantName;
            LoadMenuItems();
        }

        // Default constructor (useful for designer or fallback)
        public RestaurantProfileWindow()
        {
            InitializeComponent();
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dlg.ShowDialog() == true)
            {
                imagePath = dlg.FileName;
                imgPreview.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(priceText) || !decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("⚠ Please enter a valid item name and numeric price.");
                return;
            }

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = @"INSERT INTO MenuItems (RestaurantId,  ItemName, Price, Description)
                             VALUES (@RestaurantId, @ItemName, @Price, @Description)";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            //cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);
            cmd.Parameters.AddWithValue("@ItemName", itemName);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Description", description);
           // cmd.Parameters.AddWithValue("@ImagePath", imagePath ?? "");

            cmd.ExecuteNonQuery();
            MessageBox.Show("✅ Menu item added successfully.");
            ClearInputs();
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

            dataGridMenu.ItemsSource = null;
            dataGridMenu.ItemsSource = menuItems;
           

        }

        private void dataGridMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGridMenu.SelectedItem is MenuItemModel selectedItem)
            {
                txtItemName.Text = selectedItem.ItemName;
                txtPrice.Text = selectedItem.Price.ToString("F2");
                txtDescription.Text = selectedItem.Description;
            }
        }

        private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMenu.SelectedItem is not MenuItemModel selectedItem)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            string itemName = txtItemName.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(priceText) || !decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("⚠ Please enter a valid item name and price.");
                return;
            }

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = @"UPDATE MenuItems 
                             SET ItemName = @ItemName, Price = @Price, Description = @Description 
                             WHERE MenuItemId = @MenuItemId";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@ItemName", itemName);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@MenuItemId", selectedItem.MenuItemId);

            cmd.ExecuteNonQuery();

            MessageBox.Show("✔ Menu item updated.");
            ClearInputs();
            LoadMenuItems();
        }

        private void ClearInputs()
        {
            txtItemName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            imgPreview.Source = null;
            imagePath = "";
        }

        private void DeliveryManagement_Click(object sender, RoutedEventArgs e)
        {
            DeliveryTrackingWindow page = new DeliveryTrackingWindow();
            page.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtRestaurantName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }

        private void txtDescription_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void UpdateRestaurant_Click(object sender, RoutedEventArgs e)
        {
           
            string newName = txtRestaurantName.Text.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("⚠ Please enter a valid restaurant name.");
                return;
            }
            using SqlConnection conn = new(connectionString);
            conn.Open();
            string query = @"UPDATE Restaurant SET RestaurantName = @RestaurantName WHERE Id = @RestaurantId";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@RestaurantName", newName);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("✔ Restaurant name updated successfully.");
            lblRestaurant.Text = newName;
           
        }
    }

    // Model for menu item
    public class MenuItemModel
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
