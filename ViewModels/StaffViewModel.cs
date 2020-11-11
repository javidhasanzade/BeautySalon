using System;
using BeautySalon.Services;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Models;
using System.Windows;
using BeautySalon.Commands;
using BeautySalon.Views;
using System.Linq;
using System.Collections.Generic;

namespace BeautySalon.ViewModels
{
    public class StaffList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        public int BranchId { get; set; }
        public string PositionTitle { get; set; }

    }

    class StaffViewModel : BaseViewModel
    {
        private List<StaffList> staff;
        public List<StaffList> Staff { get => staff; set => OnChanged(out staff, value); }
        public StaffViewModel()
        {
            UpdateTable();
        }

        private BaseCommand addStaffCommand = null;
        private BaseCommand deleteStaffCommand = null;
        private BaseCommand editStaffCommand = null;
        private BaseCommand positionsCommand = null;

        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => OnChanged(out selectedItem, value ); }
        
        public BaseCommand DeleteStaffCommand => deleteStaffCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                using BeautySalonDBContext db = new BeautySalonDBContext();
                db.Employees.Remove(SelectedItem as Employee);
                db.SaveChanges();
                UpdateTable();
            }

        }, () => true);

        public BaseCommand EditStaffCommand => editStaffCommand ??= new BaseCommand(x =>
        {
            if (selectedItem == null)
                MessageBox.Show("Вы не выбрали");
            else
            {
                ChangeStaffWindow changeStaff = new ChangeStaffWindow(SelectedItem);
                changeStaff.ShowDialog();
                UpdateTable();
            }

        }, () => true);

        public BaseCommand AddStaffCommand => addStaffCommand ??= new BaseCommand(x =>
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.ShowDialog();
            UpdateTable();
        }, () => true);

        public BaseCommand PositionsCommand => positionsCommand ??= new BaseCommand(x =>
        {
            PositionsView positionsView = new PositionsView();
            positionsView.Show();
        }, () => true);

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Employees.LoadAsync();
            var tempEmployee = (from emp in db.Employees
                                join pos in db.Positions on emp.PositionId equals pos.Id
                                select new StaffList
                                {
                                    Id = emp.Id,
                                    Name = emp.Name,
                                    Surname = emp.Surname,
                                    PositionId = pos.Id,
                                    BranchId = emp.BranchId,
                                    PositionTitle = pos.Name
                                }).ToList();
            Staff = tempEmployee.ToList();
        }
  

        
    }

}
