using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FoodDeliverySystem.Models;

namespace FoodDeliverySystem.Views
{
    public partial class MenuForm : Window
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        private int nextId = 1;

        public MenuForm()
        {
            InitializeComponent();
            RefreshMenuGrid();
        }

        private void RefreshMenuGrid()
        {
            dgMenuItems.ItemsSource = null;
            dgMenuItems.ItemsSource = menuItems;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Enter a valid price.");
                return;
            }

            var item = new MenuItem
            {
                Id = nextId++,
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = price,
                Category = ((ComboBoxItem)cmbCategory.SelectedItem)?.Content.ToString()
            };

            menuItems.Add(item);
            RefreshMenuGrid();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgMenuItems.SelectedItem is MenuItem selectedItem)
            {
                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Enter a valid price.");
                    return;
                }

                selectedItem.Name = txtName.Text;
                selectedItem.Description = txtDescription.Text;
                selectedItem.Price = price;
                selectedItem.Category = ((ComboBoxItem)cmbCategory.SelectedItem)?.Content.ToString();

                RefreshMenuGrid();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMenuItems.SelectedItem is MenuItem selectedItem)
            {
                menuItems.Remove(selectedItem);
                RefreshMenuGrid();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void dgMenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMenuItems.SelectedItem is MenuItem selectedItem)
            {
                txtName.Text = selectedItem.Name;
                txtDescription.Text = selectedItem.Description;
                txtPrice.Text = selectedItem.Price.ToString();
                cmbCategory.SelectedItem = cmbCategory.Items
                    .OfType<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == selectedItem.Category);
            }
        }
    }
}
