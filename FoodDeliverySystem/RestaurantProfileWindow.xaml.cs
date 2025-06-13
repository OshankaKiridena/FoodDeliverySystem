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

        public RestaurantProfileWindow(int restaurantId, string restaurantName)
        {
            InitializeComponent();
            this.restaurantId = restaurantId;
            this.restaurantName = restaurantName;

            lblRestaurant.Text = restaurantName;
            LoadMenuItems();
        }

        public RestaurantProfileWindow()
        {
            InitializeComponent(); // fallback constructor
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

            string query = @"INSERT INTO MenuItems (RestaurantName, RestaurantId, ItemName, Price, Description, ImagePath)
                             VALUES (@RestaurantName, @RestaurantId, @ItemName, @Price, @Description, @ImagePath)";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            cmd.Parameters.AddWithValue("@ItemName", itemName);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@ImagePath", imagePath ?? "");

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

            string query = @"SELECT MenuItemId, ItemName, Price, Description 
                             FROM MenuItems 
                             WHERE RestaurantId = @RestaurantId";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menuItems.Add(new MenuItemModel
                {
                    MenuItemId = reader.GetInt32(0),
                    ItemName = reader.GetString(1),
                    Price = reader.GetDecimal(2),
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class MenuItemModel
    {
        public int MenuItemId { get; set; }
        public string RestaurantName { get; set; }
        public int RestaurantId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
