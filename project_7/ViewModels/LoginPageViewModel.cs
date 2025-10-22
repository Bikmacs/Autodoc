using project_7.Commands;
using project_7.Models;
using project_7.Views;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace project_7.ViewModels
{
    public class LoginPageViewModel : INotifyCollectionChanged
    {
        public ICommand LoginCommand { get; }




        private Doctor _loginDoctor = new Doctor();
        public Doctor LoginDoctor
        {
            get => _loginDoctor;
            set { _loginDoctor = value; OnPropertyChanged(nameof(LoginDoctor)); }
        }

        private Doctor _currentDoctor = new Doctor();
        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(nameof(CurrentDoctor)); }
        }

        private Pacient _currentPatient = new Pacient();
        public Pacient CurrentPatient
        {
            get => _currentPatient;
            set { _currentPatient = value; OnPropertyChanged(nameof(CurrentPatient)); }
        }


        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(_ => AuthorizationDoc());
        }

        public bool AuthorizationDoc()
        {
            string Directory = "Doctors";
            string fileName = Path.Combine(Directory,$"D_{LoginDoctor.Id}.json");
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Пользователь не найден");
                return false;
            }

            var json = File.ReadAllText(fileName);
            var doctor = JsonSerializer.Deserialize<Doctor>(json);
            if (doctor?.Password != LoginDoctor.Password)
            {
                MessageBox.Show("Неверный пароль");
                return false;
            }

            LoginDoctor = doctor;
            MessageBox.Show($"Успешный вход, {doctor.Name}");
            MainWindow.Pages?.Navigate(new MainPageDoctor(LoginDoctor));

            return true;
        }


        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
