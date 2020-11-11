using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class ServiceViewModel :BaseViewModel
    {
        private List<Service> service;
        public List<Service> Service { get => service; set => OnChanged(out service, value); }
        public ServiceViewModel()
        {
            UpdateTable();
        }

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Services.LoadAsync();
            Service = db.Services.Local.ToList();
        }

        private BaseCommand addServiceCommand = null;       

        public BaseCommand AddServiceCommand => addServiceCommand ??= new BaseCommand(x =>
        {            
            AddServiceWindow serviceWindow = new AddServiceWindow();
            serviceWindow.ShowDialog();
            UpdateTable();

        }, () => true);
        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => OnChanged(out selectedItem, value); }


        private BaseCommand deleteServiceCommand = null;
        public BaseCommand DeleteServiceCommand => deleteServiceCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                using BeautySalonDBContext db = new BeautySalonDBContext();
                db.Services.Remove(SelectedItem as Models.Service);
                db.SaveChanges();
                UpdateTable();
            }

        }, () => true);


        private BaseCommand сhangeServiceCommand = null;
        public BaseCommand ChangeServiceCommand => сhangeServiceCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                ChangeServiceWindow changeWindow = new ChangeServiceWindow(selectedItem);
                changeWindow.ShowDialog();
                UpdateTable();
            }

        }, () => true);
    }
}
