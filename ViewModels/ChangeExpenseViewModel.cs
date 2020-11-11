using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeautySalon.ViewModels
{
    class ChangeExpenseViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private object selectedItem = null;
        public ChangeExpenseViewModel(object selectedItem)
        {
            this.selectedItem = selectedItem;
            SetItems();
        }

        private string description;
        private decimal price;
        private DateTime time;

        private void SetItems()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var setItm = db.Expenses.Where(c => c.Id == (selectedItem as Models.Expense).Id).FirstOrDefault();
            description = setItm.Description;
            price = setItm.Amount;
            time = setItm.Time;
        }

        public string Des
        {
            get => description;
            set
            {
                OnChanged(out description, value);
                ChangeExpensesCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                OnChanged(out price, value);
            }
        }

        public DateTime Time
        {
            get => time;
            set
            {
                OnChanged(out time, value);
               
            }
        }

        private BaseCommand changeExpensesCommand = null;
        public BaseCommand ChangeExpensesCommand => changeExpensesCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var serv = db.Expenses.Where(c => c.Id == (selectedItem as Models.Expense).Id).FirstOrDefault();

            
            serv.Description = this.Des;
            serv.Amount = this.Price;
            serv.Time = this.Time;
            db.SaveChanges();
            CloseWindow();

        }, () => !string.IsNullOrWhiteSpace(Des));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);
    }
}
