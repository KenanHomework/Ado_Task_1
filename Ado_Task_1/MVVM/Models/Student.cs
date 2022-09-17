using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.MVVM.Models
{
    public class Student
    {

        #region Members
        private string name;
        private string surname;
        private string password;


        public int Id { get; set; }

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        public string Surname { get => surname; set { surname = value; OnPropertyChanged(); } }

        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public Student(int id, string name, string surname, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Password = password;
        }

        public Student()
        {

        }
    }

}
