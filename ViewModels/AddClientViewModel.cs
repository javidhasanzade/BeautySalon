using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class AddClientViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private string name;
        private string surname;
        private string phoneNumber = "+994";

        public string Name {
            get => name;
            set
            {
                OnChanged(out name, value);
                AddClientCommand.RaiseCanExecuteChanged();
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


        private BaseCommand addClientCommand = null;

        public BaseCommand AddClientCommand => addClientCommand ??= new BaseCommand(x =>
        {
            using (BeautySalonDBContext dBContext = new BeautySalonDBContext())
            {
                Client cli1 = new Client { Name = this.Name, Surname = this.Surname, PhoneNumber = this.PhoneNumber,BranchId=branchNum };
                dBContext.Clients.Add(cli1);
                dBContext.SaveChanges();
                MessageBox.Show("Uğurlu!:)");
            }
           
        }, () => !string.IsNullOrWhiteSpace(Name));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);

    }
}
