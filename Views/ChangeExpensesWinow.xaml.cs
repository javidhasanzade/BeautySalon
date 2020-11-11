using BeautySalon.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
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
    /// Логика взаимодействия для ChangeExpensesWinow.xaml
    /// </summary>
    public partial class ChangeExpensesWinow : Window
    {
        public ChangeExpensesWinow(object selectedItem)
        {
            InitializeComponent();
            var changeExpenseViewModel = new ChangeExpenseViewModel(selectedItem);
            changeExpenseViewModel.CloseWindow = new Action(Close);
            DataContext = changeExpenseViewModel;

        }
    }
}
