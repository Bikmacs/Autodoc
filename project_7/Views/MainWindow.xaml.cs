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
        //private void Button_Register(object sender, RoutedEventArgs e)
        //{
        //    vm.RegisterDoctor();
        //    UpdateWindow();            
        //}
        //private void Button_Login(object sender, RoutedEventArgs e)
        //{
        //    int.TryParse(LoginText.Text, out int id);
        //    vm.LoginDoctor(id, PasswordText.Password);
        //}
        //private void Button_Add_User(object sender, RoutedEventArgs e)
        //{
        //    vm.AddPatients();
        //    UpdateWindow();
        //}

        //private void Button_Change_Patient(object sender, RoutedEventArgs e)
        //{            
        //    vm.ChangePatient2();
        //}

        //private void Button_Search_User(object sender, RoutedEventArgs e)
        //{
        //    int.TryParse(SearchUser.Text, out int id);
        //    vm.SearchPatient(id);
        //}

        //private void Button_Click_Reset(object sender, RoutedEventArgs e)
        //{
        //    vm.ReserInformationPatient();
        //}

       
        //private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    vm.RegisterDoctrors.Password = ((PasswordBox)sender).Password;
        //}

        //private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    vm.RegisterDoctrors.ConfirmPassword = ((PasswordBox)sender).Password;
        //}
        //private void Button_Save_Patient(object sender, RoutedEventArgs e)
        //{
        //    vm.SavePatient(vm.CurrentPatient.LastDoctor, vm.CurrentPatient.Diagnosis, vm.CurrentPatient.Recomendations);
        //}

        //public void UpdateWindow()
        //{
        //    int filePacient = Directory.GetFiles(".", "P_*.json").Length;
        //    int fileDoctors = Directory.GetFiles(".", "D_*.json").Length;
        //    status.Text = $"Пациентов: {filePacient.ToString()}";
        //    qual.Text = $"Докторов: {fileDoctors.ToString()}";
        //}

      
        
    }

}
