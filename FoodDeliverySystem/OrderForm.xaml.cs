using System;
using System.Collections.Generic;
using System.Windows;

namespace FoodDeliverySystem
{
    public partial class OrderForm : Window
    {
        private Order currentOrder;
        private List<string> menuItems = new List<string> { "Burger", "Pizza", "Fries", "Coke" };
        private Dictionary<string, decimal> prices = new Dictionary<string, decimal>
        {
            { "Burger", 5.99m },
            { "Pizza", 8.99m },
            { "Fries", 2.99m },
            { "Coke", 1.49m }
        };

        public OrderForm()
        {
            InitializeComponent();
            currentOrder = new Order();
            ItemComboBox.ItemsSource = menuItems;
            ItemComboBox.SelectedIndex = 0;
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = ItemComboBox.SelectedItem.ToString();
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                var item = new OrderItem(selectedItem, quantity, prices[selectedItem]);
                currentOrder.AddItem(item);
                OrderListBox.Items.Add(item.ToString());
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.", "Input Error");
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order Placed Successfully!\n\n" + currentOrder.ToString(), "Success");
            currentOrder = new Order(); // Reset order
            OrderListBox.Items.Clear();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            TotalTextBlock.Text = $"Total: ${currentOrder.GetTotalPrice():F2}";
        }
    }
}
