using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    public class AppointmentsList
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int ClientId { get; set; }
        public string ClientFullname { get; set; }
        public int StaffId { get; set; }
        public string StaffFullname { get; set; }
        public int ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
    }
    class CalendarViewModel : BaseViewModel
    {
        private List<AppointmentsList> appointment;
        public List<AppointmentsList> Appointments { get => appointment; set => OnChanged(out appointment, value); }

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Appointments.LoadAsync();
            var tempApp = (from app in db.Appointments
                           join emp in db.Employees on app.StaffId equals emp.Id
                           join cli in db.Clients on app.ClientId equals cli.Id
                           join serv in db.Services on app.ServiceId equals serv.Id
                           where app.BranchId == branchNum
                           select new AppointmentsList
                           {
                               Id = app.Id,
                               BranchId = app.BranchId,
                               ClientId = app.ClientId,
                               ClientFullname = cli.Fullname,
                               StaffId = app.StaffId,
                               StaffFullname = emp.Fullname,
                               ServiceId = app.ServiceId,
                               ServiceTitle = serv.Title,
                               Date = app.Date,
                               StartTime = app.StartTime,
                               EndTime = app.EndTime,
                               Price = app.Price,
                               Notes = app.Notes,
                               Status = app.Status

                           }).ToList();
            Appointments = tempApp.Where(d => d.Date == SelectedDate.Date).ToList();
        }

        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate { get => selectedDate; set { OnChanged(out selectedDate, value); } }
       
        // TODO: TEST
        public CalendarViewModel()
        {
            //MessageBox.Show(branchNum.ToString());
            UpdateTable();
            //ShowFullName();
        }

        private BaseCommand addAppointmentCommand = null;
        private BaseCommand deleteAppointmentCommand = null;
        private BaseCommand testCommand = null;
        public BaseCommand TestCommand => testCommand ??= new BaseCommand(x =>
        {
            UpdateTable();

        }, () => true);


        private int status;
        public int Status
        {
            get => status;
            set
            {
                OnChanged(out status, value);
            }
        }


        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => OnChanged(out selectedItem, value); }

        private BaseCommand doubleClickCommand = null;
        public BaseCommand DoubleClickCommand => doubleClickCommand ??= new BaseCommand(x =>
        {
            AppointmentStatysWindow aps = new AppointmentStatysWindow(SelectedItem as AppointmentsList);
            aps.ShowDialog();
            UpdateTable();
        }, () => true);

        public BaseCommand AddAppointmentCommand => addAppointmentCommand ??= new BaseCommand(x =>
        {
            AddAppointmentWindow addAppointment = new AddAppointmentWindow();
            addAppointment.ShowDialog();
            UpdateTable();

        }, () => true);

        public BaseCommand DeleteAppointment => deleteAppointmentCommand ??= new BaseCommand(x =>
        {
            try
            {
                using BeautySalonDBContext db = new BeautySalonDBContext();
                db.Appointments.Remove(SelectedItem as Appointment);
                db.SaveChanges();
                UpdateTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не выбрали");
            }


        }, () => true);


    }
}
