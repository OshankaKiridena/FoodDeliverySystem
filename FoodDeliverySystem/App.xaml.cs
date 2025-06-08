using System.Windows;

namespace FoodDeliveryApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}

