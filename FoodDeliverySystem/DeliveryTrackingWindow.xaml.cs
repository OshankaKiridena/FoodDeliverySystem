using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FoodDeliveryApp
{
    public partial class DeliveryTrackingWindow : Window
    {
        public List<DeliveryModel> Deliveries { get; set; } = new();
        public List<string> DeliveryStaffList { get; set; } = new() { "Ravi", "Amal", "Nimesh" };

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

            foreach (var delivery in Deliveries)
            {
                delivery.DeliveryStaffList = DeliveryStaffList;
            }

            dgDeliveries.ItemsSource = Deliveries;
        }

        private void AssignDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeliveries.SelectedItem is DeliveryModel selected)
            {
                MessageBox.Show($"Assigned {selected.AssignedTo} to Order {selected.OrderId}.");
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
                selected.Status = "Delivered";
                dgDeliveries.Items.Refresh();
                MessageBox.Show($"Updated status of Order {selected.OrderId} to Delivered.");
            }
            else
            {
                MessageBox.Show("Select an order to update.");
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
}
