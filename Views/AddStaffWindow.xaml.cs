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
    /// Interaction logic for AddStaffWindow.xaml
    /// </summary>
    public partial class AddStaffWindow : Window
    {
        public AddStaffWindow()
        {
            InitializeComponent();
            var addStaffViewModel = new AddStaffViewModel();
            addStaffViewModel.CloseWindow = new Action(Close);
            DataContext = addStaffViewModel;
        }

      
    }
}
