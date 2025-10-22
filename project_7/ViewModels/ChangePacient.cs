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

namespace project_7.ViewModels
{
    public class ChangePacient : INotifyPropertyChanged
    {
        //Сделать обновление страницы на главном экране 
        //Переделать кнопки на команды а  именно в логине регистрации и во всех изменениях кроме истории болезней

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

        public const string Directory = "Pacient";


        public ChangePacient(Pacient pacient)
        {
            AddPacient = pacient;
        }

        public ChangePacient()
        {
            AddPacient = new Pacient
            {
                Birthday = DateTime.Today
            };
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
            MessageBox.Show($"Пациент {AddPacient.Name} успешно зарегистрирован!\nID: {AddPacient.Id}");

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

            MessageBox.Show($"Пациент {AddPacient.Name} сохранён!\nID: {AddPacient.Id}");
        }


        private int GeneratePacientId() => new Random().Next(1000000, 9999999);

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
