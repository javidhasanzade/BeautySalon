using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Linq;

namespace BeautySalon.ViewModels
{
    class ChangeClientViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private object selectedItem = null;
        public ChangeClientViewModel(object selectedItem)  
        {
            this.selectedItem = selectedItem;
            SetItems();
        }

        public void SetItems()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var cl = db.Clients.Where(c => c.Id == (selectedItem as Client).Id).FirstOrDefault();
            Name = cl.Name;
            Surname = cl.Surname;
            PhoneNumber = cl.PhoneNumber;
        }

        private string name;
        private string surname;
        private string phoneNumber;

        public string Name
        {
            get => name;
            set
            {
                OnChanged(out name, value);
                ChangeClientCommand.RaiseCanExecuteChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                OnChanged(out surname, value);
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                OnChanged(out phoneNumber, value);
            }
        }

        private BaseCommand changeClientCommand = null;
        public BaseCommand ChangeClientCommand => changeClientCommand ??= new BaseCommand(x =>
        {
            
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var cl = db.Clients.Where(c => c.Id == (selectedItem as Client).Id).FirstOrDefault();
            cl.Name = this.Name;
            cl.Surname = this.Surname;
            cl.PhoneNumber = this.PhoneNumber;
            db.SaveChanges();

        }, () => !string.IsNullOrWhiteSpace(Name));
        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);
    }
}
