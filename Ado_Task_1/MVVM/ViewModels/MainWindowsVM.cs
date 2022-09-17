using ADO_Lesson_1.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ADO_Lesson_1.Commands;
using System.Data.SqlClient;
using ADO_Lesson_1.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Printing;
using System.Windows;
using System.Windows.Media;
using ADO_Lesson_1.MVVM.Views;
using ADO_Lesson_1.Services;
using System.Collections;

namespace ADO_Lesson_1.MVVM.ViewModels
{
    public class MainWindowsVM : IResetable, IRefreshable
    {

        #region Members

        private string name;
        private string surname;
        private string password;
        private List<Student> students = new();
        private int selectedIndex = -1;

        public MainWindow Window { get; set; }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Surname { get => surname; set { surname = value; OnPropertyChanged(); } }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }
        public List<Student> Students { get => students; set { students = value; OnPropertyChanged(); } }



        public string ToolTipStr { get; set; } = "Password Requirements :\n* Use at least 8 characters \n* Use upper and lower case characters \n* Use 1 or more numbers \n* Recommend use special characters";

        #endregion

        #region Commands

        public RelayCommand Add { get; set; }

        public RelayCommand CommandRefresh { get; set; }

        #endregion

        #region Methods

        public void ReadSql()
        {
            try
            {
                SqlConnection conn = new(App.Options.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("use StudentsDB;select * from Students", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Students = new();
                while (reader.Read())
                {
                    Students.Add(new Student()
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Surname = (string)reader[2],
                        Password = (string)reader[3]
                    });
                }
                conn.Close();

            }
            catch (Exception) { }
            Refresh();
        }

        public void AddRun(object param)
        {
            try
            {
                SqlConnection conn = new(App.Options.ConnectionString);
                conn.Open();
                string q = $"use StudentsDB;insert into Students values(@Id ,@Name, @Surname , @Password )";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.Add(new() { ParameterName = "@Id", Value = GetLastId(), SqlDbType = System.Data.SqlDbType.Int });
                cmd.Parameters.Add(new() { ParameterName = "@Name", Value = Name, SqlDbType = System.Data.SqlDbType.VarChar });
                cmd.Parameters.Add(new() { ParameterName = "@Surname", Value = Surname, SqlDbType = System.Data.SqlDbType.VarChar });
                cmd.Parameters.Add(new() { ParameterName = "@Password", Value = Password, SqlDbType = System.Data.SqlDbType.VarChar });
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception) { }
            ReadSql();
            Reset();
            Refresh();
        }

        public void SelectionChanged()
        {
            if (SelectedIndex < 0) return;
            EditStudentView es = new();
            App.Container.GetInstance<EditStudentVM>().Student = new(Students[SelectedIndex].Id, Students[SelectedIndex].Name, Students[SelectedIndex].Surname, Students[SelectedIndex].Password);
            App.Container.GetInstance<EditStudentVM>().CStudent = new(Students[SelectedIndex].Id, Students[SelectedIndex].Name, Students[SelectedIndex].Surname, Students[SelectedIndex].Password);
            es.ShowDialog();
            if (es.Student == null) return;
            try
            {
                SqlConnection conn = new(App.Options.ConnectionString);
                conn.Open();
                string q = $"use StudentsDB;update Students Set [Name] = '{es.Student.Name}',Surname = '{es.Student.Surname}',[Password] = '{es.Student.Password}' where Id = {SelectedIndex}";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception) { }
            ReadSql();
            App.Container.GetInstance<EditStudentVM>().Reset();
            Refresh();
        }

        public int GetLastId()
        {
            ReadSql();
            if (Students.Count == 0) return 0;
            return Students.Last().Id + 1;
        }

        public bool AddCanRun(object param) => AllInfoCorrect();

        public bool AllInfoCorrect() =>
            !string.IsNullOrWhiteSpace(Surname) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(Name) &&
            Regex.IsMatch(Name, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Surname, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        public void RefreshRun(object param) => Refresh();

        void RefreshListView()
        {
            Window.StudentsListView.ItemsSource = null;
            Window.StudentsListView.ItemsSource = Students;
        }

        #endregion

        #region Implements

        public void Reset()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Password = string.Empty;

            Window.NameTBX.Text = Name;
            Window.SurnameTBX.Text = Surname;
            Window.HidePassword.Password = Password;
            Window.ShowPassword.Text = Password;
            Window.NameTBX.Foreground = new SolidColorBrush(Color.FromRgb(237, 236, 239));
            Window.SurnameTBX.Foreground = new SolidColorBrush(Color.FromRgb(237, 236, 239));
            Window.HidePassword.Foreground = new SolidColorBrush(Color.FromRgb(237, 236, 239));
            Window.ShowPassword.Foreground = new SolidColorBrush(Color.FromRgb(237, 236, 239));
        }

        public void Refresh()
        {
            RefreshListView();
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        public MainWindowsVM()
        {
            Add = new RelayCommand(AddRun, AddCanRun);
            CommandRefresh = new(RefreshRun);

        }

    }
}
