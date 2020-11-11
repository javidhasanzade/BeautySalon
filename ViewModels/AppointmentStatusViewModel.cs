using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BeautySalon.ViewModels
{
    internal class AppointmentStatusViewModel : BaseViewModel
    {
        private readonly AppointmentsList appointment;
        public string ServiceTitle { get; private set; }
        public string MasterFullname { get; private set; }
        public decimal Price { get; private set; }
        public decimal BranchEarning { get; set; }
        public decimal StaffEarning { get; set; }
        public AppointmentStatusViewModel(AppointmentsList app1)
        {
            this.appointment = app1;
            //using BeautySalonDBContext dbContext = new BeautySalonDBContext();
            //var service = dbContext.Services.Where(x => x.Id == appointment.ServiceId);
            ServiceTitle = appointment.ServiceTitle;
            MasterFullname = appointment.StaffFullname;
            Price = appointment.Price;
        }
        private BaseCommand checkoutCommand = null;
        public BaseCommand CheckoutCommand => checkoutCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            var tempApp = dBContext.Appointments.Find(appointment.Id);
            tempApp.Checkout = DateTime.UtcNow;
            tempApp.Status = 1;
            dBContext.Appointments.Update(tempApp);
            StaffEarning staffEarning1 = new StaffEarning { Amount = StaffEarning, BranchId = branchNum, EmployeeId = appointment.StaffId, AppointmentId = appointment.Id };
            if (dBContext.Services.Find(tempApp.ServiceId).IsLaser == 1)
            {
                LaserEarning laserEarning1 = new LaserEarning { Amount = BranchEarning, BranchId = branchNum, AppointmentId = appointment.Id };
                dBContext.LaserEarnings.Add(laserEarning1);
            }
            else if (dBContext.Services.Find(tempApp.ServiceId).IsLaser == 0)
            {
                Earning branchEarning1 = new Earning { Amount = BranchEarning, BranchId = branchNum, AppointmentId = appointment.Id };
                dBContext.Earnings.Add(branchEarning1);
            }
            dBContext.StaffEarnings.Add(staffEarning1);
            dBContext.SaveChanges();
        }, () => true);

        private BaseCommand cancelCommand = null;
        public BaseCommand CancelCommand => cancelCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            var tempApp = dBContext.Appointments.Find(appointment.Id);
            tempApp.Status = -1;
            dBContext.Appointments.Update(tempApp);
            dBContext.SaveChanges();

        }, () => true);

    }
}
