using System.Windows;

namespace FoodDeliverySystem
{
    public partial class ReviewWindow : Window
    {
        public ReviewWindow()
        {
            InitializeComponent();
        }

        private void SubmitReview_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string review = txtReview.Text.Trim();
            int rating = cbRating.SelectedIndex + 1;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(review) || cbRating.SelectedIndex == -1)
            {
                MessageBox.Show("Please complete all fields and select a rating.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Simple in-memory add; for real apps, save to DB.
            string formatted = $"⭐ {rating}/5 by {name}: {review}";
            lstReviews.Items.Insert(0, formatted); // Newest on top

            // Optionally clear fields
            txtName.Clear();
            txtReview.Clear();
            cbRating.SelectedIndex = -1;
        }
    }
}
