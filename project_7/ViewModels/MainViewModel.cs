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
using System.Xml.Serialization;

namespace project_7.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
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

        private Pacient _addPatient = new Pacient();
        public Pacient AddPatient
        {
            get => _addPatient;
            set { _addPatient = value; OnPropertyChanged(nameof(AddPatient)); }
        }

        private Pacient _changePatient = new Pacient();
        public Pacient ChangePatients
        {
            get => _changePatient;
            set { _changePatient = value; OnPropertyChanged(nameof(ChangePatients)); }
        }

        private Doctor _registerDoctrors = new Doctor();
        public Doctor RegisterDoctrors
        {
            get => _registerDoctrors;
            set { _registerDoctrors = value; OnPropertyChanged(nameof(RegisterDoctrors)); }
        }


        private bool AuthorizationFlag = false;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        //public void RegisterDoctor()
        //{
        //    if (CurrentDoctor.Password != CurrentDoctor.ConfirmPassword)
        //    {
        //        MessageBox.Show("Пароли не равны");
        //        return;
        //    }

        //    if (RegisterDoctrors.Name == null || RegisterDoctrors.LastName == null || RegisterDoctrors.MiddleName == null || RegisterDoctrors.Password == null || RegisterDoctrors.Specialisation == null)
        //    {
        //        MessageBox.Show("Поля пустые");
        //    }
        //    else
        //    {
        //        RegisterDoctrors.Id = GenerateId();
        //        string fileName = $"D_{RegisterDoctrors.Id}.json";
        //        string jsonString = JsonSerializer.Serialize(RegisterDoctrors, new JsonSerializerOptions
        //        {
        //            WriteIndented = true,
        //            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //        });
        //        File.WriteAllText(fileName, jsonString);
        //        MessageBox.Show($"Доктор {RegisterDoctrors.Name} успешно зарегистрирован!\nID: {RegisterDoctrors.Id}");
        //    }
        //}

        //public void LoginDoctor(int id, string password)
        //{
        //    string fileName = $"D_{id}.json";
        //    if (!File.Exists(fileName))
        //    {
        //        MessageBox.Show("Пользователь не найден");
        //        return;
        //    }

        //    var json = File.ReadAllText(fileName);
        //    var doctor = JsonSerializer.Deserialize<Doctor>(json);
        //    if (doctor?.Password != password)
        //    {
        //        MessageBox.Show("Неверный пароль");
        //        return;
        //    }

        //    CurrentDoctor = doctor;
        //    AuthorizationFlag = true;
        //    MessageBox.Show($"Успешный вход, {doctor.Name}");
        //}
        //public void AddPatients()
        //{
        //    if (AuthorizationFlag == false) MessageBox.Show("Вы не авторизованы");
        //    else
        //    {
        //        AddPatient.Id = GenerateUserId();
        //        string fileName = $"P_{AddPatient.Id}.json";
        //        string jsonString = JsonSerializer.Serialize(AddPatient, new JsonSerializerOptions
        //        {
        //            WriteIndented = true,
        //            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //        });
        //        File.WriteAllText(fileName, jsonString);
        //        MessageBox.Show($"Пациент {AddPatient.Name} успешно зарегистрирован!\nID: {AddPatient.Id}");
        //    }
        //}


        //public void SearchPatient(int id)
        //{
        //    if(AuthorizationFlag == false) MessageBox.Show("Вы не авторизованы");
        //    else
        //    {
        //        string fileName = $"P_{id}.json";
        //        if (!File.Exists(fileName))
        //        {
        //            MessageBox.Show("Пользователь не найден");
        //            return;
        //        }
        //        string jsonString = File.ReadAllText(fileName);
        //        CurrentPatient = JsonSerializer.Deserialize<Pacient>(jsonString)!;
        //    }
        //}

        //public void SavePatient(string lastDoc, string diagnos,string recomen)
        //{
        //    if (AuthorizationFlag == false) MessageBox.Show("Вы не авторизованы");
        //    else
        //    {
        //        CurrentPatient.LastDoctor = lastDoc;
        //        CurrentPatient.Diagnosis = diagnos;
        //        CurrentPatient.Recomendations = recomen;

        //        string fileName = $"P_{CurrentPatient.Id}.json";

        //        if (File.Exists(fileName))
        //        {
        //            var oldFile = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
        //            if (oldFile != null)
        //            {
        //                oldFile.LastDoctor = CurrentPatient.LastDoctor;
        //                oldFile.Diagnosis = CurrentPatient.Diagnosis;
        //                oldFile.Recomendations = CurrentPatient.Recomendations;
        //                oldFile.LastAppointment = DateOnly.FromDateTime(DateTime.Now);
        //                CurrentPatient = oldFile;
        //            }
        //        }


        //        string jsonString = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
        //        {
        //            WriteIndented = true,
        //            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //        });
        //        File.WriteAllText(fileName, jsonString);
        //        MessageBox.Show($"Пациент {CurrentPatient.Name} сохранен!\nID: {CurrentPatient.Id}");
        //    }
        //}
        //public void ChangePatient2()
        //{
        //    if (AuthorizationFlag == false) MessageBox.Show("Вы не авторизованы");
        //    else
        //    {
        //        string fileName = $"P_{CurrentPatient.Id}.json";

        //        if (File.Exists(fileName))
        //        {
        //            var oldFile2 = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
        //            if (oldFile2 != null)
        //            {
        //                oldFile2.Name = ChangePatients.Name;
        //                oldFile2.Diagnosis = ChangePatients.LastName;
        //                oldFile2.Recomendations = ChangePatients.MiddleName;
        //                oldFile2.Birthday = ChangePatients.Birthday;
        //                ChangePatients = oldFile2;
        //            }
        //        }
        //        string jsonString = JsonSerializer.Serialize(ChangePatients, new JsonSerializerOptions
        //        {
        //            WriteIndented = true,
        //            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //        });
        //        File.WriteAllText(fileName, jsonString);
        //        MessageBox.Show($"Пациент {ChangePatients.Name} сохранен!\nID: {ChangePatients.Id}");
        //    }
        //}
        //public void ReserInformationPatient()
        //{
        //    if (AuthorizationFlag == false) MessageBox.Show("Вы не авторизованы");
        //    else
        //    {
        //        string fileName = $"P_{CurrentPatient.Id}.json";
        //        if (File.Exists(fileName))
        //        {
        //            var file = JsonSerializer.Deserialize<Pacient>(File.ReadAllText(fileName));
        //            if (file != null)
        //            {
        //                file.LastAppointment = CurrentPatient.LastAppointment;
        //                file.LastDoctor = CurrentPatient.LastDoctor;
        //                file.Diagnosis = CurrentPatient.Diagnosis;
        //                file.Recomendations = CurrentPatient.Recomendations;
        //                CurrentPatient = file;
        //            }

        //        }
        //        else { MessageBox.Show("Файл не найден!"); }
        //    }

        //}

      

        private int GenerateId() => new Random().Next(10000, 99999);
    }
}
