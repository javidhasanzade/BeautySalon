using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySalon.ViewModels
{
    class ChangeStaffViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private object selectedItem = null;
        private string name;
        private string surname;
        private List<Position> positions;
        private Position selectedPosition;
        public string Name { get => name; set => OnChanged(out name, value); }
        public string Surname { get => surname; set => OnChanged(out surname, value); }
        public List<Position> Positions { get => positions; set => OnChanged(out positions, value); }
        public Position SelectedPosition { get => selectedPosition; set => OnChanged(out selectedPosition, value); }

        public ChangeStaffViewModel(object selectedItem)
        {
            this.selectedItem = selectedItem;
            SetItems();
        }

        public void SetItems()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var cl = db.Employees.Where(c => c.Id == (selectedItem as StaffList).Id).FirstOrDefault();
            Name = cl.Name;
            Surname = cl.Surname;
            Positions = db.Positions.ToList();
        }

        private BaseCommand saveCommand = null;
        public BaseCommand SaveCommand => saveCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var cl = db.Employees.Where(c => c.Id == (selectedItem as StaffList).Id).FirstOrDefault();
            cl.Name = this.Name;
            cl.Surname = this.Surname;
            cl.PositionId = SelectedPosition.Id;
            db.SaveChanges();
            CloseWindow();
        }, () => true);

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow();
        }, () => true);
    }
}
