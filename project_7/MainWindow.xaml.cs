﻿using project_7.Models;
using project_7.ViewModels;
using System.DirectoryServices;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_7
{
    /// <summary>
    /// Логика 
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainViewModel VM { get; set; } = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }
        private void Button_Register(object sender, RoutedEventArgs e)
        {
            VM.RegisterDoctor();
        }
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            int.TryParse(LoginText.Text, out int id);
            VM.LoginDoctor(id, PasswordText.Password);
        }
        private void Button_Add_User(object sender, RoutedEventArgs e)
        {
            string Name = AddName.Text;
            string LastName = AddLastName.Text;
            string MiddleName = AddMiddleName.Text;
            DateOnly birthday = AddBirthday.SelectedDate.HasValue
                ? DateOnly.FromDateTime(AddBirthday.SelectedDate.Value)
                : DateOnly.FromDateTime(DateTime.Now);

            VM.AddPatient(Name, LastName, MiddleName, birthday);
        }
        private void Button_Search_User(object sender, RoutedEventArgs e)
        {
            int.TryParse(SearchUser.Text, out int id);
            VM.SearchPatient(id);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            VM.CurrentDoctor.Password = ((PasswordBox)sender).Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            VM.CurrentDoctor.ConfirmPassword = ((PasswordBox)sender).Password;
        }
        private void Button_Save_Patient(object sender, RoutedEventArgs e)
        {
            VM.SavePatient(VM.CurrentPatient.LastDoctor, VM.CurrentPatient.Diagnosis, VM.CurrentPatient.Recomendations);
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            Name.Text = string.Empty;
            LastName.Text = string.Empty;
            MiddleName.Text = string.Empty;
            Birthday.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Change_Patient(object sender, RoutedEventArgs e)
        {
            string changeName = Name.Text;
            string changeLastName = LastName.Text;
            string changeMiddleName = MiddleName.Text;
            DateTime selectedDate = Birthday.SelectedDate ?? DateTime.Now;
            DateOnly dateOnly = DateOnly.FromDateTime(selectedDate);
            VM.ChangePatient(changeName, changeLastName, changeMiddleName, dateOnly);
        }
    }

}
// поиск по авторизации 
// вернуть в последнее состояние файла