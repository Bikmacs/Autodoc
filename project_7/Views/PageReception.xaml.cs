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
    /// Логика взаимодействия для PageReception.xaml
    /// </summary>
    public partial class PageReception : Page
    {
        private ReceptioPageVievModel VMReception { get; set; }
        
        public PageReception(Pacient pacient, Doctor doctor)
        {
            InitializeComponent();
            VMReception = new ReceptioPageVievModel(pacient, doctor); 
            DataContext = VMReception;
        }
        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DiagnosisBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
