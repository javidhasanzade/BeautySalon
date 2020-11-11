using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class AddServiceViewModel : BaseViewModel
    {

        private string title;
        private decimal price;
        private TimeSpan duration;
        private Boolean isLaser = true;
        private Boolean nonLaser;

        public string Title { get => title; set { OnChanged(out title, value); AddServiceCommand.RaiseCanExecuteChanged(); } }
        public decimal Price { get => price; set => OnChanged(out price, value); }             
        public TimeSpan Duration { get => duration; set => OnChanged(out duration, value); }
        public bool IsLaser { get => isLaser; set => OnChanged(prop: out isLaser, value); }
        public bool NonLaser { get => nonLaser; set => OnChanged(out nonLaser, value); }
        public Action CloseWindow { get; set; }

        private BaseCommand addServiceCommand = null;
        public BaseCommand AddServiceCommand => addServiceCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            Service ser1 = new Service { Title = this.Title, Price = this.Price, Duration = this.Duration, IsLaser = Convert.ToInt32(IsLaser) };
            dBContext.Services.Add(ser1);
            dBContext.SaveChanges();
            MessageBox.Show("Success!");

        }, () => !string.IsNullOrWhiteSpace(Title));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow.Invoke();
        }, () => true);

    }
}
