using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class AddExpensesViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private string description;
        private decimal amount;
        private DateTime time;

        public string Description
        {
            get => description;
            set
            {
                OnChanged(out description, value);
                AddDescriptionCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal Amount { get => amount; set => OnChanged(out amount, value); }

        private DateTime Time
        {
            get => time;
            set
            {
                OnChanged(out time, value);
            }            
        }

        private BaseCommand addDescriptionCommand = null;
        public BaseCommand AddDescriptionCommand => addDescriptionCommand ??= new BaseCommand(x =>
        {

            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            Expense exp1 = new Expense
            {
                Description = this.Description,
                Amount = this.Amount,
                Time = this.Time,
                BranchId = branchNum
            };
            dBContext.Expenses.Add(exp1);
            dBContext.SaveChanges();
            MessageBox.Show("Success!");

        }, () => !string.IsNullOrWhiteSpace(Description));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);


    }
}
