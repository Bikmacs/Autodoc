using project_7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace project_7.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public void RegisterDoctor()
        {
            if (CurrentDoctor.Password != CurrentDoctor.ConfirmPassword)
            {
                MessageBox.Show("Пароли не равны");
                return;
            }

            CurrentDoctor.Id = GenerateId();
            string fileName = $"D_{CurrentDoctor.Id}.json";
            string jsonString = JsonSerializer.Serialize(CurrentDoctor, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);
            MessageBox.Show($"Доктор {CurrentDoctor.Name} успешно зарегистрирован!\nID: {CurrentDoctor.Id}");
        }

        public void LoginDoctor(int id, string password)
        {
            string fileName = $"D_{id}.json";
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            var json = File.ReadAllText(fileName);
            var doctor = JsonSerializer.Deserialize<Doctor>(json);
            if (doctor?.Password != password)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            CurrentDoctor = doctor;
            MessageBox.Show($"Успешный вход, {doctor.Name}");
        } 

        public void AddPatient(string name, string lastName, string middleName, DateOnly birthday)
        {
            CurrentPatient.Id = GenerateId();
            CurrentPatient.Name = name;
            CurrentPatient.LastName = lastName;
            CurrentPatient.MiddleName = middleName;
            CurrentPatient.Birthday = birthday;

            CurrentPatient.Id = GenerateUserId();
            string fileName = $"P_{CurrentPatient.Id}.json";
            string jsonString = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);
            MessageBox.Show($"Пациент {CurrentPatient.Name} успешно зарегистрирован!\nID: {CurrentPatient.Id}");
        }

        public void SearchPatient(int id)
        {
            string fileName = $"P_{id}.json";
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            string jsonString = File.ReadAllText(fileName);
            CurrentPatient = JsonSerializer.Deserialize<Pacient>(jsonString)!;
        }

        public void SavePatient(string lastDoc, string diagnos,string recomen)
        {
            CurrentPatient.LastDoctor = lastDoc;
            CurrentPatient.Diagnosis = diagnos;
            CurrentPatient.Recomendations = recomen;

            string fileName = $"P_{CurrentPatient.Id}.json";

            if(File.Exists(fileName))
            {
                var oldFile = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
                if(oldFile != null)
                {
                    oldFile.LastDoctor = CurrentPatient.LastDoctor;
                    oldFile.Diagnosis = CurrentPatient.Diagnosis;
                    oldFile.Recomendations = CurrentPatient.Recomendations;
                    oldFile.LastAppointment = DateOnly.FromDateTime(DateTime.Now);
                    CurrentPatient = oldFile;
                }
            }

            string jsonString = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);
            MessageBox.Show($"Пациент {CurrentPatient.Name} сохранен!\nID: {CurrentPatient.Id}");
        }

        public void ChangePatient(string changeName, string changeLastName, string changeMiddleName, DateOnly dateOnly)
        {
            CurrentPatient.Name = changeName;
            CurrentPatient.LastName = changeLastName;
            CurrentPatient.MiddleName = changeMiddleName;
            CurrentPatient.Birthday = dateOnly;

            string fileName = $"P_{CurrentPatient.Id}.json";

            if (File.Exists(fileName))
            {
                var oldFile2 = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
                if (oldFile2 != null)
                {
                    oldFile2.Name = CurrentPatient.Name;
                    oldFile2.Diagnosis = CurrentPatient.LastName;
                    oldFile2.Recomendations = CurrentPatient.MiddleName;
                    oldFile2.Birthday = CurrentPatient.Birthday;
                    CurrentPatient = oldFile2;
                }
            }

            string jsonString = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(fileName, jsonString);
            MessageBox.Show($"Пациент {CurrentPatient.Name} сохранен!\nID: {CurrentPatient.Id}");
        }

         private int GenerateId() => new Random().Next(10000, 99999);
         private int GenerateUserId() => new Random().Next(1000000, 9999999);
    }
}
