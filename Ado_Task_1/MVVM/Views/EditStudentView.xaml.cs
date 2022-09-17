using ADO_Lesson_1.MVVM.Models;
using ADO_Lesson_1.MVVM.ViewModels;
using ADO_Lesson_1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADO_Lesson_1.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EditStudentView.xaml
    /// </summary>
    public partial class EditStudentView : Window
    {
        public EditStudentView()
        {
            InitializeComponent();
            App.Container.GetInstance<EditStudentVM>().Window = this;
            DataContext = App.Container.GetInstance<EditStudentVM>();
        }

        public Student Student { get; set; } = null;

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => this.Close();

        private void NameTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref NameTBX, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");

        private void SurnameTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref SurnameTBX, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");


        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
    }
}
