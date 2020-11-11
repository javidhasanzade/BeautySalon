using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class ClientViewModel : BaseViewModel
    {
        private List<Client> client;
        public List<Client> Client { get => client; set => OnChanged(out client, value); }

        public ClientViewModel()
        {
            UpdateTable();
        }

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Clients.LoadAsync();
            Client = db.Clients.Where(x => x.BranchId == branchNum).ToList();
        }

        private BaseCommand addClientCommand = null;
        public BaseCommand AddClientCommand => addClientCommand ??= new BaseCommand(x =>
         {
             AddClientWindow clientWindow = new AddClientWindow();
             clientWindow.ShowDialog();
             UpdateTable();
         }, () => true);


        private BaseCommand deleteClient = null;
        private object selectedItem;

        public object SelectedItem { get => selectedItem; set => OnChanged(out selectedItem, value); }
        public BaseCommand DeleteClientCommand => deleteClient ??= new BaseCommand(x =>
        {
            try
            {
                using BeautySalonDBContext db = new BeautySalonDBContext();
                db.Clients.Remove(SelectedItem as Client);
                db.SaveChanges();
                UpdateTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не выбрали");
            }


        }, () => true);

        private BaseCommand changeClientCommand = null;
        public BaseCommand ChangeClientCommand => changeClientCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                ChangeClientWindow changeClient = new ChangeClientWindow(selectedItem);
                changeClient.ShowDialog();
                UpdateTable();
            }

        }, () => true);



    }
}
