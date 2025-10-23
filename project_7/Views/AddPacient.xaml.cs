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
    /// Логика взаимодействия для AddPacient.xaml
    /// </summary>
    public partial class AddPacient : Page
    {
        public ChangePacient CP { get; set; }
        public AddPacient(MainPageDoctorViewModel mainPage)
        {
            InitializeComponent();
            CP = new ChangePacient
            {
                MainPage = mainPage,
            };
            DataContext = CP;
        }

        private void Button_Add_Pacient(object sender, RoutedEventArgs e)
        {
            CP.AddPatients();
        }
    }
}
