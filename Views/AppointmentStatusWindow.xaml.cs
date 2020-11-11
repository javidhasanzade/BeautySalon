using BeautySalon.Models;
using BeautySalon.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeautySalon.Views
{
    /// <summary>
    /// Логика взаимодействия для AppointmentStatysWindow.xaml
    /// </summary>
    public partial class AppointmentStatysWindow : Window
    {
        public AppointmentStatysWindow(AppointmentsList app1)
        {
            InitializeComponent();
            DataContext = new AppointmentStatusViewModel(app1);
        }
    }
}
