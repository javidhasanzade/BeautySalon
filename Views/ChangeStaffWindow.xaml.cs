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
    /// Логика взаимодействия для ChangeStaffWindow.xaml
    /// </summary>
    public partial class ChangeStaffWindow : Window
    {
        public ChangeStaffWindow(object selectedItem)
        {
            InitializeComponent();
            var changeStaffViewModel = new ChangeStaffViewModel(selectedItem);
            changeStaffViewModel.CloseWindow = new Action(Close);
            DataContext = changeStaffViewModel;
        }
    }
}
