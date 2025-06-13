// RestaurantProfileWindow.xaml.cs
using FoodDeliverySystem;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;

using System.Collections.Generic;
using System.Data.SqlClient;

using System.Windows;
using System.Configuration;
using System.Windows.Media.Imaging;

namespace FoodDeliveryApp

{
    public partial class RestaurantProfileWindow : Window
    {
        //private int staffId;
        private int restaurantId;
        private string restaurantName;
        private string imagePath = "";
        private string connectionString = "Server=localhost;Database=FoodDeliveryDB;Trusted_Connection=True;";
        private List<MenuItemModel> menuItems = new List<MenuItemModel>();
        private object restaurantDetails;

        public RestaurantProfileWindow(int restaurantId, string restaurantName)
        {
            InitializeComponent();
            // this.staffId = staffId;
            this.restaurantId = restaurantId;
            this.restaurantName = restaurantName;

            lblRestaurant.Text = restaurantName;
            LoadMenuItems();
        }
        public RestaurantProfileWindow()
        {
            InitializeComponent();
        }



        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
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
                MessageBox.Show("Please enter valid item name and price.");
                return;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO MenuItems (RestaurantName,RestaurantDetails,RestaurantId, ItemName, Price, Description, ImagePath)\n    SELECT @restaurantName, @restaurantId,@restaurantDetails, @item, @price, @desc, @img FROM MenuItems ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@RestaurantDetails", restaurantDetails);
                    cmd.Parameters.AddWithValue("@item", itemName);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@img", imagePath);
                    //cmd.Parameters.AddWithValue("@staffId", staffId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Menu item added successfully.");

                    ClearInputs();
                    LoadMenuItems();
                }
            }
        }

        private void LoadMenuItems()
        {
            menuItems.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT m.MenuItemId, m.ItemName, m.Price, m.Description FROM MenuItems m
                                 JOIN Restaurants r ON m.RestaurantId = r.RestaurantId
                                 WHERE r.StaffId = @staffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@staffId", staffId);
                    SqlDataReader reader = cmd.ExecuteReader();
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
            }
        }

        private void dataGridMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGridMenu.SelectedItem is MenuItemModel selectedItem)
            {
                txtItemName.Text = selectedItem.ItemName;
                txtPrice.Text = selectedItem.Price.ToString();
                txtDescription.Text = selectedItem.Description;
            }
        }

        private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMenu.SelectedItem is MenuItemModel selectedItem)
            {
                string itemName = txtItemName.Text.Trim();
                string priceText = txtPrice.Text.Trim();
                string description = txtDescription.Text.Trim();

                if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(priceText) || !decimal.TryParse(priceText, out decimal price))
                {
                    MessageBox.Show("Please enter valid item name and price.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE MenuItems SET ItemName=@item, Price=@price, Description=@desc WHERE MenuItemId=@id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item", itemName);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@id", selectedItem.MenuItemId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Menu item updated.");
                ClearInputs();
                LoadMenuItems();
            }
            else
            {
                MessageBox.Show("Please select an item to update.");
            }
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
    }

    public class MenuItemModel
    {
        public string RestaurantName { get; set; }
        public string RestaurantDetails { get; set; }
        public int RestaurantId { get; set; }
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        
    }
}


    
