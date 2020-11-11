using BeautySalon.Commands;
using BeautySalon.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class StaffEarningProceed : BaseViewModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        private decimal earning;
        public decimal Earning { get => earning; set => OnChanged(out earning, value); }
        private decimal branchProceed;
        public decimal BranchProceed { get => branchProceed; set => OnChanged(out branchProceed, value); }
        private decimal laserProceed;
        public decimal LaserProceed { get => laserProceed; set => OnChanged(out laserProceed, value); }
        public List<int> appointmentsId = new List<int>();
    }

    class StaffEarningViewModel : BaseViewModel
    {
        private List<StaffEarningProceed> staffEarningProceeds;
        private DateTime fromSelectedDate;
        private DateTime toSelectedDate;
        public List<StaffEarningProceed> StaffEarningProceeds { get => staffEarningProceeds; set => OnChanged(out staffEarningProceeds, value); }
        public DateTime FromSelectedDate { get => fromSelectedDate; set => OnChanged(out fromSelectedDate, value); }
        public DateTime ToSelectedDate { get => toSelectedDate; set => OnChanged(out toSelectedDate, value); }
        public StaffEarningViewModel()
        {
            staffEarningProceeds = new List<StaffEarningProceed>();
            fromSelectedDate = DateTime.UtcNow;
            toSelectedDate = DateTime.UtcNow;
            LoadStaff();
            LoadEarnings();
        }

        //private async void UpdateTable()
        //{
        //    using BeautySalonDBContext db = new BeautySalonDBContext();
        //    await db.Employees.LoadAsync();
        //    //var sep = (from emp in db.Employees
        //    //           join apps in db.Appointments on emp.Id equals apps.StaffId
        //    //           join earn in db.Earnings on apps.Id equals earn.AppointmentId
        //    //           join staffEarn in db.StaffEarnings on apps.Id equals staffEarn.AppointmentId
        //    //           where emp.BranchId == branchNum
        //    //           select new StaffEarningProceed
        //    //           {
        //    //               Fullname = emp.Fullname,
        //    //               Earning = earn.
        //    //           }).ToList();
        //}

        private void LoadEarnings()
        {
            
            using (BeautySalonDBContext db = new BeautySalonDBContext())
            {
                for (int i = 0; i < StaffEarningProceeds.Count; i++)
                {
                    for (int j = 0; j < db.Appointments.ToList().Count; j++)
                    {
                        if (StaffEarningProceeds[i].Id == db.Appointments.ToList()[j].StaffId && db.Appointments.ToList()[j].Date >= FromSelectedDate &&
                            db.Appointments.ToList()[j].Date <= ToSelectedDate)
                            StaffEarningProceeds[i].appointmentsId.Add(db.Appointments.ToList()[j].Id);
                    }
                }

                for (int i = 0; i < StaffEarningProceeds.Count; i++)
                {
                    for (int k = 0; k < StaffEarningProceeds[i].appointmentsId.Count; k++)
                    {
                        for (int j = 0; j < db.Earnings.ToList().Count; j++)
                        {
                            if (db.Earnings.ToList()[j].AppointmentId == StaffEarningProceeds[i].appointmentsId[k])
                                StaffEarningProceeds[i].BranchProceed += db.Earnings.ToList()[j].Amount;
                        }

                        for (int j = 0; j < db.LaserEarnings.ToList().Count; j++)
                        {
                            if (db.LaserEarnings.ToList()[j].AppointmentId == StaffEarningProceeds[i].appointmentsId[k])
                                StaffEarningProceeds[i].LaserProceed += db.LaserEarnings.ToList()[j].Amount;
                        }

                        for (int j = 0; j < db.StaffEarnings.ToList().Count; j++)
                        {
                            if (db.StaffEarnings.ToList()[j].AppointmentId == StaffEarningProceeds[i].appointmentsId[k])
                                StaffEarningProceeds[i].Earning += db.StaffEarnings.ToList()[j].Amount;
                        }
                    }
                }
            }
        }

        private void LoadStaff()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            foreach (var emp in db.Employees.ToList())
            {
                if (branchNum == emp.BranchId)
                {
                    StaffEarningProceed staffEarningProceed = new StaffEarningProceed
                    {
                        Id = emp.Id,
                        Fullname = emp.Fullname,
                        Earning = 0,
                        BranchProceed = 0
                    };
                    staffEarningProceeds.Add(staffEarningProceed);
                }
            }
        }

        private BaseCommand selectedDateChangedCommand = null;
        public BaseCommand SelectedDateChangedCommand => selectedDateChangedCommand ??= new BaseCommand(x =>
        {
            foreach(var i in StaffEarningProceeds)
            {
                i.appointmentsId.Clear();
                i.Earning = 0;
                i.BranchProceed = 0;
            }
            LoadEarnings();

        }, () => true);
    }
}
