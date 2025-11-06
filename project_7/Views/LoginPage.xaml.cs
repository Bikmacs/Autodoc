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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPageViewModel lpv = new LoginPageViewModel();
        public LoginPage()
        {
            InitializeComponent();
            DataContext = lpv;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            lpv.AuthorizationDoc();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        { 
           lpv.LoginDoctor.Password = ((PasswordBox)sender).Password;
        }


        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
