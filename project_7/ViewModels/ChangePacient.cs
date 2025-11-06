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

namespace project_7.ViewModels
{
    public class ChangePacient : INotifyPropertyChanged
    {
        public MainPageDoctorViewModel MainPage { get; set; }

        

        private Pacient _addPacient = new Pacient();
        public Pacient AddPacient
        {
            get => _addPacient;
            set { _addPacient = value; OnPropertyChanged(nameof(AddPacient)); }
        }

        private Pacient SelectedPacient = new Pacient();
        public Pacient _selectedPacient
        {
            get => _selectedPacient;
            set { _selectedPacient = value; OnPropertyChanged(nameof(SelectedPacient)); }
        }

        private Doctor _currentDoctor = new Doctor();
        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(nameof(CurrentDoctor)); }
        }

        public ICommand AddPacientViewCommand { get; }
        public ICommand ChangePacBack { get; }
        public ICommand Back { get; }

        public const string Directory = "Pacient";


        public ChangePacient(Pacient pacient)
        {
            AddPacient = pacient;
            ChangePacBack = new RelayCommand(_ => ChangeByPacient());
        }
        public ChangePacient()
        {
            AddPacient = new Pacient
            {
                Birthday = DateTime.Today
            };
            AddPacientViewCommand = new RelayCommand(_ => AddPatients());
            Back = new RelayCommand(_ => MainWindow.Pages?.GoBack());
        }


        public void AddPatients()
        {
            AddPacient.Id = GeneratePacientId();

            string fileName = Path.Combine(Directory, $"P_{AddPacient.Id}.json");
            string jsonString = JsonSerializer.Serialize(AddPacient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);
            MessageBoxResult result = MessageBox.Show(
                $"Пациент {AddPacient.Name} успешно зарегистрирован!{Environment.NewLine}ID: {AddPacient.Id}",
                "Регистрация",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information );

            if (result == MessageBoxResult.OK)
            {
                MainWindow.Pages?.GoBack();
                MainPage?.PacientList.Add(AddPacient);
            }

        }

        public void ChangeByPacient()
        {
            string fileName = Path.Combine(Directory, $"P_{AddPacient.Id}.json");

            if (File.Exists(fileName))
            {
                var oldPacient = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
                if (oldPacient != null)
                {
                    oldPacient.Name = AddPacient.Name;
                    oldPacient.LastName = AddPacient.LastName;
                    oldPacient.MiddleName = AddPacient.MiddleName;
                    oldPacient.Birthday = AddPacient.Birthday;
                    AddPacient = oldPacient;
                }
            }

            string jsonString = JsonSerializer.Serialize(AddPacient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);

            MessageBoxResult result = MessageBox.Show(
                $"Пациент {AddPacient.Name} сохранён!",
                "Cохранение",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                MainWindow.Pages?.GoBack();
                MainPage?.PacientList.Add(AddPacient);
            }

        }


        private int GeneratePacientId() => new Random().Next(1000000, 9999999);

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
