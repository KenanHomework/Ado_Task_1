using ADO_Lesson_1.Enums;
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
    /// Interaction logic for EnterAdminPasswordView.xaml
    /// </summary>
    public partial class EnterAdminPasswordView : Window
    {
        public EnterAdminPasswordView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool Succes { get; set; } = false;

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
            ((EnterAdminPasswordView)DataContext).Password = HidePassword.Password;
        }

        private void Submite_Click(object sender, RoutedEventArgs e)
        {
            if (Password != "Admin123")
            {
                CMessageBox.Show("Incorrect Admin Password !", CMessageTitle.Warning, CMessageButton.Ok, CMessageButton.None);
                Succes = false;
                return;
            }
            Succes = true;
            this.Close();
        }
    }
}
