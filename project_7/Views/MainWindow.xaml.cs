using project_7.Models;
using project_7.ViewModels;
using project_7.Views;
using System.DirectoryServices;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_7
{
    /// <summary>
    /// Логика 
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainViewModel vm { get; set; } = new MainViewModel();
        public static Frame? Pages;

        public MainWindow()
        {
            InitializeComponent();
            Pages = MainFrame;
            Pages.Navigate(new LoginPage());    

            MainFrame.Navigate(new LoginPage());
            DataContext = vm;
        }

        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Toggle();
        }

    }

}
