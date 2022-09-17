using ADO_Lesson_1.Commands;
using ADO_Lesson_1.MVVM.Models;
using ADO_Lesson_1.MVVM.Views;
using ADO_Lesson_1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADO_Lesson_1.MVVM.ViewModels
{
    public class OptionsWindowVM
    {

        #region Members

        private Options options = new();

        public OptionsWindow Window { get; set; }

        public Options Options { get => options; set { options = value; OnPropertyChanged(); } }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand Save { get; set; }

        #endregion

        #region Methods

        public void SaveRun(object param)
        {
            Options.ConnectionString = TextService.GetText(Options.ConnectionString, '\\', 2);
            App.Options = this.Options;
            if (Options.RememberOptions == true) App.Options.Save();
            App.ToMainWindow();
        }

        public bool SaveCanRun(object param) => !string.IsNullOrWhiteSpace(Options.ConnectionString);

        #endregion


        public OptionsWindowVM()
        {
            Save = new(SaveRun, SaveCanRun);
        }
    }
}
