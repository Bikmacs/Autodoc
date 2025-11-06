using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace project_7.Models
{
    public class Pacient : INotifyPropertyChanged
    {
        public ObservableCollection<AppointmentStories> AppointmentStores { get; set; } = new ObservableCollection<AppointmentStories>();

        public Pacient()
        {
            AppointmentStores.CollectionChanged += OnAppointmentStoresChanged;
        }
        private void OnAppointmentStoresChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(DaysSinceLastAppointment));
        }

        private int? _id;
        public int? Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _middleName = string.Empty;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AgeFormat));
                }
            }
        }


        public string AgeFormat
        {
            get
            {
                DateTime now = DateTime.Today;
                DateTime birthDate = Birthday.Value.Date;
                int age = now.Year - birthDate.Year;
                if (birthDate > now.AddYears(-age)) age-- ;
              
                return age >= 18 ? "Совершеннолетний" : "Несовершеннолетний";
            }
        }



        private string? _phoneNumber;
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DaysSinceLastAppointment
        {
            get
            {
                if (AppointmentStores == null || !AppointmentStores.Any())
                {
                    return "Первый прием в клинике";
                }

                DateTime? lastDate = AppointmentStores
                    .Where(a => a.Date.HasValue && a.Date.Value.Date <= DateTime.Today)
                    .OrderByDescending(a => a.Date.Value.Date)
                    .Select(a => a.Date.Value.Date)
                    .FirstOrDefault();

                if (lastDate == null || !lastDate.HasValue)
                {
                    return "Первый прием в клинике";
                }

                DateTime lastAppointmentDate = lastDate.Value;
                DateTime today = DateTime.Today;    

                TimeSpan difference = today - lastAppointmentDate;
                int valueDay = (int)difference.TotalDays;

                if (valueDay == 0)
                {
                    return "Сегодня"; 
                }

                return $"{valueDay} дней назад";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
