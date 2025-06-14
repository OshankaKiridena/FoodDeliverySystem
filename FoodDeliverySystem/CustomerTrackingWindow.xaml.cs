using System;
using System.Windows;

namespace FoodDeliveryApp
{
    public partial class CustomerTrackingWindow : Window
    {
        public CustomerTrackingWindow()
        {
            InitializeComponent();
        }

        private void TrackOrder_Click(object sender, RoutedEventArgs e)
        {
            // Simulate tracking (replace with database/real data as needed)
            string orderId = txtOrderId.Text.Trim();

            if (string.IsNullOrEmpty(orderId))
            {
                lblStatus.Text = "Please enter a valid Order ID.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                progressOrder.Value = 0;
                lblEta.Text = "";
                return;
            }

            // --- Simulated Statuses (in real app, query the database) ---
            // For demo, just randomly assign a status:
            var rand = new Random(orderId.GetHashCode());
            int status = rand.Next(0, 4);

            switch (status)
            {
                case 0:
                    lblStatus.Text = "Order placed (waiting for restaurant confirmation)";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Gray;
                    progressOrder.Value = 20;
                    lblEta.Text = "Estimated delivery: --";
                    break;
                case 1:
                    lblStatus.Text = "Your food is being prepared";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Orange;
                    progressOrder.Value = 50;
                    lblEta.Text = "Estimated delivery: 30 minutes";
                    break;
                case 2:
                    lblStatus.Text = "Order is out for delivery";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Blue;
                    progressOrder.Value = 80;
                    lblEta.Text = "Estimated delivery: 10 minutes";
                    break;
                case 3:
                    lblStatus.Text = "Delivered! Enjoy your meal!";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Green;
                    progressOrder.Value = 100;
                    lblEta.Text = "Delivered now";
                    break;
            }
        }
    }
}
