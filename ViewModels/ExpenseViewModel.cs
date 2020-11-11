using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class ExpenseViewModel : BaseViewModel
    {
        private ObservableCollection<Expense> expenses;
        public ObservableCollection<Expense> Expenses { get => expenses; set => OnChanged(out expenses, value); }

        public ExpenseViewModel()
        {
            UpdateTable();
        }

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Expenses.LoadAsync();
            Expenses = db.Expenses.Local.ToObservableCollection();
        }


        private BaseCommand addExpenseCommand = null;

        public BaseCommand AddExpenseCommand => addExpenseCommand ??= new BaseCommand(x =>
        {
            AddExpensesWindow expWindow = new AddExpensesWindow();
            expWindow.ShowDialog();
            UpdateTable();
        }, () => true);


        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => OnChanged(out selectedItem, value); }

        private BaseCommand deleteExpenceCommand = null;
        public BaseCommand DeleteExpenceCommand => deleteExpenceCommand ??= new BaseCommand(x=>{
            try
            {
                using BeautySalonDBContext db = new BeautySalonDBContext();
                db.Expenses.Remove(SelectedItem as Expense);
                db.SaveChanges();
                UpdateTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не выбрали");
            }

        });
        
        private BaseCommand changeExpenseCommand = null;
        public BaseCommand ChangeExpenceCommand => changeExpenseCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                ChangeExpensesWinow changeClient = new ChangeExpensesWinow(selectedItem);
                changeClient.ShowDialog();
                UpdateTable();
            }

        }, () => true);

    }
}
