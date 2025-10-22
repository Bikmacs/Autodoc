using project_7.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_7.Views
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPageViewModel rpv = new RegPageViewModel();
        public RegPage()
        {
            InitializeComponent();
            DataContext = rpv;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            rpv.RegisterDoctors.Password = ((PasswordBox)sender).Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            rpv.RegisterDoctors.ConfirmPassword = ((PasswordBox)sender).Password;
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            rpv.RegisterDoctor();
        }
    }
}
