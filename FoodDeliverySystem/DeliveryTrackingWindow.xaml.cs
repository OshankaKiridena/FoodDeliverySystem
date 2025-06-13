using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FoodDeliveryApp
{
    public partial class DeliveryTrackingWindow : Window
    {
        public List<DeliveryModel> Deliveries { get; set; } = new();
        private readonly DeliveryController _controller = new();
        public List<string> DeliveryStaffList { get; } = new() { "Ravi", "Amal", "Nimesh" };

        public DeliveryTrackingWindow()
        {
            InitializeComponent();
            LoadDeliveries();
            DataContext = this; // For data binding (especially ComboBox ItemsSource)
        }

        private void LoadDeliveries()
        {
            Deliveries = new List<DeliveryModel>
            {
                new DeliveryModel { OrderId = 101, CustomerName = "John", Status = "Pending", AssignedTo = null },
                new DeliveryModel { OrderId = 102, CustomerName = "Lily", Status = "Dispatched", AssignedTo = "Ravi" }
            };

            dgDeliveries.ItemsSource = Deliveries;
        }

        private void AssignDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeliveries.SelectedItem is DeliveryModel selected)
            {
                try
                {
                    string selectedStaff = selected.AssignedTo;

                    if (string.IsNullOrEmpty(selectedStaff) || !DeliveryStaffList.Contains(selectedStaff))
                        throw new ArgumentException("Please select a valid delivery person.");

                    _controller.AssignDelivery(selected, selectedStaff);
                    dgDeliveries.Items.Refresh();

                    MessageBox.Show($"✅ Assigned {selectedStaff} to Order #{selected.OrderId}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"⚠️ Error: {ex.Message}", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to assign.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeliveries.SelectedItem is DeliveryModel selected)
            {
                try
                {
                    _controller.UpdateDeliveryStatus(selected, "Delivered");
                    dgDeliveries.Items.Refresh();

                    MessageBox.Show($"📦 Order #{selected.OrderId} marked as Delivered.", "Status Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"⚠️ Error: {ex.Message}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class DeliveryModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
    }
}
