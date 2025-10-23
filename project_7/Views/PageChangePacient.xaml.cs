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
    /// Логика взаимодействия для PageChangePacient.xaml
    /// </summary>
    public partial class PageChangePacient : Page
    {
        public ChangePacient VMChange { get; set; }
        public PageChangePacient(Pacient pacient)
        {
            InitializeComponent();
            VMChange = new ChangePacient(pacient);
            DataContext = VMChange;
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            VMChange.AddPacient = new Pacient();
        }

     
    }
}
