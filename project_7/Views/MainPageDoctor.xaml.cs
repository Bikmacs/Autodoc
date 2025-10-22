using project_7.Models;
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
    /// Логика взаимодействия для MainWindowDoctor.xaml
    /// </summary>
    public partial class MainPageDoctor : Page
    {
        public MainPageDoctorViewModel VMDoctor { get; set; }
        public MainPageDoctor(Doctor doctor)
        {            
            InitializeComponent();

            VMDoctor = new MainPageDoctorViewModel(doctor);
            DataContext = VMDoctor;

            VMDoctor.ListPacient();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            VMDoctor.PageAddPacient();
        }

        private void Start_CLick(object sender, RoutedEventArgs e)
        {
            VMDoctor.PageRecep();
        }

        private void ChangeItem_CLick(object sender, RoutedEventArgs e)
        {
            
        }
        private void EditItem_Click(object sender, RoutedEventArgs e)
        {

            //if (SelectedUser == null)
            //{
            //    MessageBox.Show("Не выбран элемент списка");
            //    return;
            //}
            //NavigationService.Navigate(new UserFormPage(Users, SelectedUser));
        }

       
    }
}
