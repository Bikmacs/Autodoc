using project_7.Commands;
using project_7.Models;
using project_7.ViewModels;
using project_7.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainPageDoctorViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pacient> PacientList { get; set; } = new ObservableCollection<Pacient>();

        public ICommand AddPacientCommand { get; }
        public ICommand StartReceptionCommand { get; }
        public ICommand EditPacientCommand { get; }


        private Doctor _currentDoctor = new Doctor();
        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(nameof(CurrentDoctor)); }
        }

        private Pacient? _selectedPacient;
        public Pacient? SelectedPacient
        {
            get => _selectedPacient;
            set
            {
                _selectedPacient = value;
                OnPropertyChanged(nameof(SelectedPacient));
            }
        }
        
        public MainPageDoctorViewModel(Doctor doctor)
        {
            CurrentDoctor = doctor;
            AddPacientCommand = new RelayCommand(_ => PageAddPacient());

            StartReceptionCommand = new RelayCommand(_ =>
            {
                if (SelectedPacient == null)
                {
                    MessageBox.Show("Не выбран пациент!");
                    return;
                }

                MainWindow.Pages?.Navigate(new PageReception(SelectedPacient, CurrentDoctor));
            });

            EditPacientCommand = new RelayCommand(_ =>
            {
                if (SelectedPacient == null)
                {
                    MessageBox.Show("Не выбран пациент!");
                    return;
                }

                MainWindow.Pages?.Navigate(new PageChangePacient(SelectedPacient));
            });

            ListPacient();
        }           


        public void ListPacient()
        {
            PacientList.Clear();
            var files = Directory.EnumerateFiles("Pacient", "*.json", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var pacient = JsonSerializer.Deserialize<Pacient>(json);
                if(pacient != null)
                {
                    PacientList.Add(pacient);
                }
            }
        }

        public void PageAddPacient()
        {
            
            MainWindow.Pages?.Navigate(new AddPacient(this));
        }

        public void PageRecep()
        {
            if (SelectedPacient != null) MainWindow.Pages?.Navigate(new PageReception(SelectedPacient, CurrentDoctor)); else return;
        }

        private void EditPacient()
        {
            if (SelectedPacient != null) MainWindow.Pages?.Navigate(new PageChangePacient(SelectedPacient)); else return;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}


