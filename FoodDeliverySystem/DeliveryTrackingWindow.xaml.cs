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
        }

        private void LoadDeliveries()
        {
            Deliveries = new List<DeliveryModel>
            {
                new DeliveryModel { OrderId = 101, CustomerName = "John", Status = "Pending", AssignedTo = null },
                new DeliveryModel { OrderId = 102, CustomerName = "Lily", Status = "Dispatched", AssignedTo = "Ravi" }
            };
            dgDeliveries.ItemsSource = Deliveries; // Redundant now due to DataContext, but kept for clarity
        }

        private void AssignDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeliveries.SelectedItem is DeliveryModel selected)
            {
                try
                {
                    string selectedStaff = selected.AssignedTo; // Now gets from ComboBox selection
                    if (string.IsNullOrEmpty(selectedStaff) || !DeliveryStaffList.Contains(selectedStaff))
                    {
                        throw new ArgumentException("Please select a valid delivery person.");
                    }
                    _controller.AssignDelivery(selected, selectedStaff);
                    dgDeliveries.Items.Refresh();
                    MessageBox.Show($"Assigned {selectedStaff} to Order {selected.OrderId}.");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select an order to assign.");
            }
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeliveries.SelectedItem is DeliveryModel selected)
            {
                try
                {
                    _controller.UpdateDeliveryStatus(selected, "Delivered"); // Could add a UI for status selection
                    dgDeliveries.Items.Refresh();
                    MessageBox.Show($"Updated status of Order {selected.OrderId} to Delivered.");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select an order to update.");
            }
        }
    }
}

public class DeliveryModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public List<string> DeliveryStaffList { get; set; }
    }

