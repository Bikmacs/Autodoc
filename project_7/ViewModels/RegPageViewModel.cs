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
using System.Windows.Controls;
using System.Windows.Input;

namespace project_7.ViewModels
{
    public class RegPageViewModel : INotifyPropertyChanged
    {
        private Doctor _registerDoctrors = new Doctor();
        public ICommand RegCommand { get; }

        public Doctor RegisterDoctors
        {
            get => _registerDoctrors;
            set { _registerDoctrors = value; OnPropertyChanged(nameof(RegisterDoctors)); }
        }

        private Doctor _currentDoctor = new Doctor();
        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(nameof(CurrentDoctor)); }
        }

        public RegPageViewModel()
        {
            RegCommand = new RelayCommand(_ => RegisterDoctor());
        }

        public void RegisterDoctor()
        {
            if (RegisterDoctors.Password != RegisterDoctors.ConfirmPassword)
            {
                MessageBox.Show("Пароли не равны");
                return;
            }


            if (RegisterDoctors.Name == null ||
                RegisterDoctors.LastName == null || 
                RegisterDoctors.MiddleName == null || 
                RegisterDoctors.Password == null ||
                RegisterDoctors.Specialisation == null)
            {
                MessageBox.Show("Поля пустые");
                return;

            }
            else
            {
                Directory.CreateDirectory("Pacient");
                Directory.CreateDirectory("Doctors");
                RegisterDoctors.Id = GenerateId();
                string fileName = Path.Combine("Doctors", $"D_{RegisterDoctors.Id}.json");

                string jsonString = JsonSerializer.Serialize(RegisterDoctors, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                File.WriteAllText(fileName, jsonString);
                MessageBox.Show($"Доктор {RegisterDoctors.Name} успешно зарегистрирован!\nID: {RegisterDoctors.Id}");
                MainWindow.Pages?.Navigate(new LoginPage());

            }
        }


        private int GenerateId() => new Random().Next(10000, 99999);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
