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
    /// Логика взаимодействия для ChangeServiceWindow.xaml
    /// </summary>
    public partial class ChangeServiceWindow : Window
    {
        public ChangeServiceWindow(object selectedItem)
        {
            InitializeComponent();
            var changeServiceViewModel = new ChangeServiceViewModel(selectedItem);
            changeServiceViewModel.CloseWindow = new Action(Close);
            DataContext = changeServiceViewModel;
        }
    }
}
