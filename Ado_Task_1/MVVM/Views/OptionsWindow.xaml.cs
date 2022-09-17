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
using System.Windows.Shapes;

namespace ADO_Lesson_1.MVVM.Views
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            App.Container.GetInstance<OptionsWindowVM>().Window = this;
            App.Container.GetInstance<OptionsWindowVM>().Options.ReadData(App.Options);
            if (App.Container.GetInstance<OptionsWindowVM>().Options.RememberOptions == true) App.ToMainWindow();
            DataContext = App.Container.GetInstance<OptionsWindowVM>();
            App.OptionsWindow = this;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

    }
}
