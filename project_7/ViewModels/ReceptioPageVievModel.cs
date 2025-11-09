using project_7.Commands;
using project_7.Models;
using project_7.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace project_7.ViewModels
{
    internal class ReceptioPageVievModel: INotifyPropertyChanged
    {
        public ObservableCollection<AppointmentStories> AppoinmentHistory { get; set; } = new ObservableCollection<AppointmentStories>();

        public ICommand Back { get; }
        public ICommand SavePacientCommand { get; }


        private Pacient? _currentPatient;
        public Pacient? CurrentPatient
        {
            get => _currentPatient;
            set { _currentPatient = value; OnPropertyChanged(nameof(CurrentPatient)); }
        }

        private Doctor? _currentDoctor;
        public Doctor? CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(nameof(CurrentDoctor)); }
        }

        private AppointmentStories? _appointment;
        public AppointmentStories? Appointment
        {
            get => _appointment;
            set { _appointment = value; OnPropertyChanged(nameof(Appointment)); }
        }


        public ReceptioPageVievModel(Pacient pacient, Doctor doctor)
        {
            CurrentDoctor = doctor;
            CurrentPatient = pacient;
            Appointment = new AppointmentStories
            {
                Date = DateTime.Now,
                DoctorId = doctor?.Id,
                DoctorLastName = doctor?.LastName,
                DoctorName = doctor?.Name,
            };
            SavePacientCommand = new RelayCommand(_ =>  SavePacient());
            Back = new RelayCommand(_ => MainWindow.Pages?.GoBack());
            UpdateHistory();
        }
        public void UpdateHistory()
        {
            AppoinmentHistory.Clear();

            if (CurrentPatient?.Id == null) return;
            string fileName = Path.Combine(ChangePacient.Directory, $"P_{CurrentPatient.Id}.json");
            if (File.Exists(fileName))
            {
                try
                {
                    var historyPacient = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
                    if (historyPacient?.AppointmentStores != null)
                    {
                        foreach (var appointment in historyPacient.AppointmentStores.OrderByDescending(a => a.Date))
                        {
                            AppoinmentHistory.Add(appointment);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}");
                }
            }
        }

        public void SavePacient()
        {
            if (CurrentPatient == null) return;

            var appoinment = new AppointmentStories
            {

                Date = DateTime.Now,
                DoctorId = CurrentDoctor?.Id,
                DoctorLastName = CurrentDoctor?.LastName,
                DoctorName = CurrentDoctor?.Name,
                Diagnosis = Appointment?.Diagnosis,
                Recomendations = Appointment?.Recomendations
            };

            CurrentPatient.AppointmentStores.Add(appoinment);

            string fileName = Path.Combine(ChangePacient.Directory, $"P_{CurrentPatient.Id}.json");
            string jsonString = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);

            MessageBox.Show($"Пациент {CurrentPatient.Name} сохранен!\nID: {CurrentPatient.Id}");

            UpdateHistory();
        }



        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
