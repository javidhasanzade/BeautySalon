using BeautySalon.Commands;
using BeautySalon.Services;
using System;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class ChangeServiceViewModel: BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private object selectedItem = null;
        public ChangeServiceViewModel(object selectedItem)
        {
            this.selectedItem = selectedItem;
            SetItems();
        }

        private string title;
        private decimal price;
        private TimeSpan duration;

        private void SetItems()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var serv = db.Services.Where(c => c.Id == (selectedItem as Models.Service).Id).FirstOrDefault();
            Title = serv.Title;
            Price = serv.Price;
            Duration = serv.Duration;
        }

        public string Title
        {
            get => title;
            set
            {
                OnChanged(out title, value);
                ChangeServiceCommand.RaiseCanExecuteChanged();
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

        public TimeSpan Duration
        {
            get => duration;
            set
            {
                OnChanged(out duration, value);
                ChangeServiceCommand.RaiseCanExecuteChanged();
            }
        }

        private BaseCommand changeServiceCommand = null;

        public BaseCommand ChangeServiceCommand => changeServiceCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var serv = db.Services.Where(c => c.Id == (selectedItem as Models.Service).Id).FirstOrDefault();

            serv.Title = this.Title;
            serv.Price = this.Price;
            serv.Duration = this.Duration;
            db.SaveChanges();
            CloseWindow();

        }, () => !string.IsNullOrWhiteSpace(Title));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);
    }
}
