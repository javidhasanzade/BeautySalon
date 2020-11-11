using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BeautySalon.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private List<Appointment> appointment;
        private string branchProceeds;
        private string laserProceeds;
        private string totalProceeds;
        private string employeeSalary;
        private string totalEarning;
        private string notCompletedAppointments;
        private string completedAppointments;
        private string allAppointments;
        private string inTheProcess;
        private string expensesMoney;
        private DateTime selectedDateStart = DateTime.Now;
        private DateTime selectedDateEnd = DateTime.Now;
        public List<Appointment> Appointment { get => appointment; set => OnChanged(out appointment, value); }
        public string BranchProceeds { get => branchProceeds; set => OnChanged(out branchProceeds, value); }
        public string LaserProceeds { get => laserProceeds; set => OnChanged(out laserProceeds, value); }
        public string TotalProceeds { get => totalProceeds; set => OnChanged(out totalProceeds, value); }
        public string EmployeeSalary { get => employeeSalary; set => OnChanged(out employeeSalary, value); }
        public string TotalEarning { get => totalEarning; set => OnChanged(out totalEarning, value); }
        public string InTheProcess { get => inTheProcess; set => OnChanged(out inTheProcess, value); }
        public string AllAppointments { get => allAppointments; set => OnChanged(out allAppointments, value); }
        public string NotCompletedAppointments { get => notCompletedAppointments; set => OnChanged(out notCompletedAppointments, value); }
        public string CompletedAppointments { get => completedAppointments; set => OnChanged(out completedAppointments, value); }
        public string ExpensesMoney { get => expensesMoney; set => OnChanged(out expensesMoney, value); }
        public DateTime SelectedDateStart { get => selectedDateStart; set => OnChanged(out selectedDateStart, value); }
        public DateTime SelectedDateEnd { get => selectedDateEnd; set { OnChanged(out selectedDateEnd, value); } }

        public HomeViewModel()
        {
            Calculate();
        }

        //public void Expense()
        //{
        //    using BeautySalonDBContext db = new BeautySalonDBContext();
        //    var app = db.Expenses;
        //    var appToDate = app.Where(d => d.Time >= SelectedDateStart && d.Time <= SelectedDateEnd && d.BranchId == branchNum);
        //    decimal azn = 0;

        //    foreach (var i in appToDate)
        //    {
        //        azn += i.Amount;
        //    }

        //    if (azn == 0)
        //    {
        //        ExpensesMoney = "0";
        //    }
        //    else
        //    {
        //        ExpensesMoney = (azn.ToString()).Substring(0, azn.ToString().Length - 2);
        //    }
        //}
        public void Calculate()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            var appointmentsList = db.Appointments.Where(d => d.Date >= SelectedDateStart && d.Date <= SelectedDateEnd && d.BranchId == branchNum);
            var expensesList = db.Expenses.Where(d => d.Time >= SelectedDateStart && d.Time <= SelectedDateEnd && d.BranchId == branchNum);
            var branchEarnings = (from e in db.Earnings
                        join app1 in db.Appointments on e.AppointmentId equals app1.Id
                        join branch1 in db.Branches on e.BranchId equals branch1.Id
                        where app1.Date >= SelectedDateStart && app1.Date <= SelectedDateEnd && e.BranchId == branchNum
                        select e);
            var laserEarnings = (from le in db.LaserEarnings
                                 join app in db.Appointments on le.AppointmentId equals app.Id
                                 join br in db.Branches on le.BranchId equals br.Id
                                 where app.Date >= SelectedDateStart && app.Date <= SelectedDateEnd && le.BranchId == branchNum
                                 select le);
            var staffEarnings = (from e in db.StaffEarnings
                             join app1 in db.Appointments on e.AppointmentId equals app1.Id
                             where app1.Date >= SelectedDateStart && app1.Date <= SelectedDateEnd && e.BranchId == branchNum
                             select e);
            decimal branchEarningsAmount = 0, laserEarningsAmount = 0, expensesAmount = 0, staffEarningsAmount = 0;

            int canceled = 0;
            int completed = 0;
            int countProcess = 0;

            foreach (var i in appointmentsList)
            {
                if (i.Status == -1)
                {
                    canceled++;
                }
                else if (i.Status == 1)
                {
                    completed++;
                }
                else if (i.Status == 0)
                {
                    countProcess++;
                }
            }

            foreach (var i in expensesList)
            {
                expensesAmount += i.Amount;
            }

            foreach(var i in branchEarnings)
            {
                branchEarningsAmount += i.Amount;
            }

            foreach (var i in laserEarnings)
            {
                laserEarningsAmount += i.Amount;
            }

            foreach (var i in staffEarnings)
            {
                staffEarningsAmount += i.Amount;
            }

            var totalEarningsAmount = branchEarningsAmount + laserEarningsAmount;
            TotalEarning = (totalEarningsAmount + staffEarningsAmount).ToString();

            if (branchEarningsAmount != 0) BranchProceeds = (branchEarningsAmount.ToString())[0..^2];
            else BranchProceeds = "0";

            if (laserEarningsAmount != 0) LaserProceeds = (laserEarningsAmount.ToString())[0..^2];
            else LaserProceeds = "0";

            if (totalEarningsAmount != 0) TotalProceeds = (totalEarningsAmount.ToString())[0..^2];
            else TotalProceeds = "0";

            if (expensesAmount != 0) ExpensesMoney = (expensesAmount.ToString())[0..^2];
            else ExpensesMoney = "0";

            if (staffEarningsAmount != 0) EmployeeSalary = (staffEarningsAmount.ToString())[0..^2];
            else EmployeeSalary = "0";

            if (Convert.ToDecimal(TotalEarning) != 0) TotalEarning = (TotalEarning.ToString())[0..^2];
            else TotalEarning = "0";

            NotCompletedAppointments = canceled.ToString();
            CompletedAppointments = completed.ToString();
            InTheProcess = countProcess.ToString();
            AllAppointments = (canceled + completed + countProcess).ToString();
        }

        private BaseCommand selectedDateChangedCommand = null;
        public BaseCommand SelectedDateChangedCommand => selectedDateChangedCommand ??= new BaseCommand(x =>
        {
            Calculate();

        }, () => true);

        private BaseCommand staffEarningsProceedsCommand = null;
        public BaseCommand StaffEarningsProceedsCommand => staffEarningsProceedsCommand ??= new BaseCommand(x =>
        {
            StaffEarningsView staffEarningsView = new StaffEarningsView();
            staffEarningsView.Show();
        }, () => true);
    }
}
