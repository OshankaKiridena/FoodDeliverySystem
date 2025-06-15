Food Delivery System - WPF C# Project

This is a WPF-based Food Delivery System built in C# as part of an academic group project. The application allows customers to browse restaurants, place orders, track deliveries, and leave reviews. Admins and staff can manage orders, restaurants, and menu items through a user-friendly GUI.

Features

View available restaurants

Browse restaurant menus and add items to cart

Place and track orders in real time

Customer order tracking window

Submit restaurant reviews

Delivery management system

Staff and admin dashboard access

Technologies Used

C# .NET 6

WPF (Windows Presentation Foundation)

SQL Server for backend database

MVVM & OOP principles

Visual Studio 2022

Git & GitHub for version control

Project Structure

/FoodDeliverySystem
│
├── App.xaml / App.xaml.cs
├── MainWindow.xaml            # Main entry/login point
├── RestaurantWindow.xaml      # Displays restaurants
├── MenuItemWindow.xaml        # Displays menu items for a restaurant
├── CartWindow.xaml            # Shows selected items
├── CustomerTrackingWindow.xaml # Tracks customer orders
├── ReviewWindow.xaml          # Review form for users
├── DeliveryWindow.xaml        # Manages deliveries (teammate's part)
├── Models/                    # OrderItem.cs, Restaurant.cs, etc.
├── Controllers/               # Business logic controllers
├── Resources/                 # Gradient styles and reusable UI assets
└── DB/                        # SQL database and connection string

Team Contribution

This project was collaboratively developed by a group of four members, each handling key components such as:

Customer/Restaurant Login, etc.—Oshanka

Delivery system, etc.—Manuja

MainWindow/Menu management, etc.—Vihanga

Reviews & Customer Tracking, etc.—Shamitha

License

This is a university course project and not intended for commercial use.
