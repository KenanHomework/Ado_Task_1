using ADO_Lesson_1.Commands;
using ADO_Lesson_1.Interfaces;
using ADO_Lesson_1.MVVM.Models;
using ADO_Lesson_1.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace ADO_Lesson_1.MVVM.ViewModels
{
    public class EditStudentVM : IResetable
    {

        #region Members

        private Student student;

        public Student CStudent;

        public EditStudentView Window { get; set; }
        public SolidColorBrush InCorrectColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        public Student Student { get => student; set { student = value; OnPropertyChanged(); } }

        #endregion

        #region Commands

        public RelayCommand Save { get; set; }

        public RelayCommand ShowPass { get; set; }

        #endregion

        #region Methods

        public void SaveRun(object param)
        {
            Window.Student = Student;
            Window.Close();
        }

        public void Reset()
        {
            ShowPassword(false);
        }

        public void ShowPassRun(object param)
        {
            EnterAdminPasswordView enterAdmin = new();
            enterAdmin.ShowDialog();
            if (enterAdmin.Succes == false) return;
            ShowPassword(true);
        }

        public void ShowPassword(bool show)
        {
            Window.ShowPass.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            Window.ShowPassword.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool SaveCanRun(object param) => AllInfoCorrect() && !AllInfoSame();

        public bool AllInfoCorrect()
=>
            !string.IsNullOrWhiteSpace(Window.NameTBX.Text) &&
            !string.IsNullOrWhiteSpace(Window.ShowPassword.Text) &&
        !string.IsNullOrWhiteSpace(Window.SurnameTBX.Text) &&
            Regex.IsMatch(Window.NameTBX.Text, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Window.SurnameTBX.Text, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Window.ShowPassword.Text, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        public bool AllInfoSame()
            => Window.NameTBX.Text == CStudent.Name &&
                Window.SurnameTBX.Text == CStudent.Surname &&
                Window.ShowPassword.Text == CStudent.Password;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public EditStudentVM()
        {
            Save = new(SaveRun, SaveCanRun);
            ShowPass = new(ShowPassRun);
            //Save = new(SaveRun);
        }

    }
}
