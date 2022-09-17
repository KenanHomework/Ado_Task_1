using ADO_Lesson_1.MVVM.Models;
using ADO_Lesson_1.MVVM.ViewModels;
using ADO_Lesson_1.MVVM.Views;
using ADO_Lesson_1.Services;
using ADO_Lesson_1.Querys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Text;

namespace ADO_Lesson_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Members

        public static SimpleInjector.Container Container = new();
        public static string SuccesSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661935108/WolfTaxi/success-sound-effect.mp3";
        public static string ErrorSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661936264/WolfTaxi/error-sound.mp3";
        public static string NotificationSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661940169/WolfTaxi/notification-sound.mp3";
        public static Options Options = new();
        public static MainWindow MainWindow;
        public static OptionsWindow OptionsWindow;


        #endregion

        #region Methods

        void Register()
        {
            Container.RegisterSingleton<MainWindowsVM>();
            Container.RegisterSingleton<EditStudentVM>();
            Container.RegisterSingleton<OptionsWindowVM>();

            Container.Verify();
        }

        public static void CheckSQl()
        {
            bool result;

            SqlService.ExecuteNonQuery(QuerysStorage.CheckDB);

            result = SqlService.ExecuteNonQuery(QuerysStorage.BackUpDifferent);
            if(!result) SqlService.ExecuteNonQuery(QuerysStorage.BackUpFull);

            result = SqlService.ExecuteNonQuery(QuerysStorage.CheckTable);
            if(!result) SqlService.ExecuteNonQuery(QuerysStorage.CreateTable);
        }

        public static void ToMainWindow()
        {
            MainWindow = new();
            if (OptionsWindow != null) OptionsWindow.Close();
            App.Container.GetInstance<MainWindowsVM>().ReadSql();
            CheckSQl();
            MainWindow.ShowDialog();
        }

        #endregion

        public App()
        {
            Register();
            Options.Load();
        }

    }
}