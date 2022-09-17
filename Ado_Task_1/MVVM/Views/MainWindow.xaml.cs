using ADO_Lesson_1.Interfaces;
using ADO_Lesson_1.MVVM.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ADO_Lesson_1.Services;

namespace ADO_Lesson_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Container.GetInstance<MainWindowsVM>().Window = this;
            DataContext = App.Container.GetInstance<MainWindowsVM>();
            App.Container.GetInstance<MainWindowsVM>();
            App.Container.GetInstance<MainWindowsVM>().ReadSql();
        }
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
                        Application.Current.Shutdown();
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

        private void NameTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref NameTBX, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");

        private void SurnameTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref SurnameTBX, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");


        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        private void PasswordEye_Click(object sender, RoutedEventArgs e)
        {
            HidePassword.Visibility = PasswordEye.IsChecked == true ? Visibility.Hidden : Visibility.Visible;
            ShowPassword.Visibility = PasswordEye.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
            if (PasswordEye.IsChecked == true) ShowPassword.Text = HidePassword.Password;
            else HidePassword.Password = ShowPassword.Text;
        }

        private void HidePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref HidePassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            ((MainWindowsVM)DataContext).Password = HidePassword.Password;
        }

        private void StudentsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => App.Container.GetInstance<MainWindowsVM>().SelectionChanged();
    }
}
