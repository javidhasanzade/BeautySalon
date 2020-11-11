using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace BeautySalon.ViewModels
{
    class AddStaffViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private string name;
        private string surname;
        private List<Position> positions;
        private Position selectedPosition;

        public string Name
        {
            get => name;
            set
            {
                OnChanged(out name, value);
                AddEmployeeCommand.RaiseCanExecuteChanged();
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
        public List<Position> Positions { get => positions; set => OnChanged(out positions, value); }
        public Position SelectedPosition { get => selectedPosition; set => OnChanged(out selectedPosition, value); }

        public AddStaffViewModel()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            Positions = db.Positions.ToList();
        }

        private BaseCommand addEmployeeCommand = null;

        public BaseCommand AddEmployeeCommand => addEmployeeCommand ??= new BaseCommand(x =>
        {
            using (BeautySalonDBContext dBContext = new BeautySalonDBContext())
            {   
                Employee emp1 = new Employee { Name = this.Name, Surname = this.Surname, PositionId = SelectedPosition.Id,BranchId = branchNum };
                dBContext.Employees.Add(emp1);
                dBContext.SaveChanges();
                MessageBox.Show("Success!");
            }
        }, () => !string.IsNullOrWhiteSpace(Name));

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow.Invoke();
        }, () => true);
    }
}
