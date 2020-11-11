using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using BeautySalon.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySalon.ViewModels
{
    internal class AddAppointmentViewModel : BaseViewModel
    {
        public Action CloseWindow { get; set; }
        private List<Client> clients;
        private List<Service> services;
        private List<Employee> staff;

        public List<Client> Clients { get => clients; set => OnChanged(out clients, value); }
        public List<Service> Services { get => services; set => OnChanged(out services, value); }
        public List<Employee> Staff { get => staff; set => OnChanged(out staff, value); }


        private DateTime selectedDate;
        private DateTime selectedStartTime;
        private DateTime selectedEndTime;
        public DateTime SelectedDate { get => selectedDate; set => OnChanged(out selectedDate, value); }
        public DateTime SelectedStartTime { get => selectedStartTime; set => OnChanged(out selectedStartTime, value); }
        public DateTime SelectedEndTime { get => selectedEndTime; set => OnChanged(out selectedEndTime, value); }

        private Client selectedClient;
        private Service selectedService;
        private Employee selectedEmployee;
        private string noteText;
        private string selectedPrice;
        public string NoteText { get => noteText; set => OnChanged(out noteText, value); }
        public string SelectedPrice { get => selectedPrice; set => OnChanged(out selectedPrice, value); }
        public Client SelectedClient { get => selectedClient; set => OnChanged(out selectedClient, value); }
        public Service SelectedService { get => selectedService; set => OnChanged(out selectedService, value); }
        public Employee SelectedEmployee { get => selectedEmployee; set => OnChanged(out selectedEmployee, value); }

        public AddAppointmentViewModel()
        {
            SelectedDate = DateTime.Now;
            UpdateComboBoxValues();
        }

        private void UpdateComboBoxValues()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            Clients = db.Clients.ToList();
            Services = db.Services.ToList();
            Staff = db.Employees.ToList();
        }

        private BaseCommand addAppointmentCommand = null;
        public BaseCommand AddAppointmentCommand => addAppointmentCommand ??= new BaseCommand(x=>
        {
            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            TimeSpan start = new TimeSpan(selectedStartTime.Hour, selectedStartTime.Minute, selectedStartTime.Second);
            TimeSpan end = new TimeSpan(selectedEndTime.Hour, selectedEndTime.Minute, selectedEndTime.Second);
            Appointment app1 = new Appointment
            {
                BranchId = branchNum,
                ClientId = SelectedClient.Id,
                StaffId = SelectedEmployee.Id,
                ServiceId = SelectedService.Id,
                StartTime = start,
                EndTime = end,
                Date = SelectedDate,
                Notes = NoteText,
                Price = Convert.ToDecimal(SelectedPrice),
                Status = 0
            };
            dBContext.Appointments.Add(app1);
            dBContext.SaveChanges();

            CloseWindow();

        }, () => true);

        private BaseCommand openAddClientWindow = null;
        public BaseCommand OpenAddClientWindow => openAddClientWindow ??= new BaseCommand(x =>
        {
            AddClientWindow addClient = new AddClientWindow();
            addClient.ShowDialog();
            UpdateComboBoxValues();
        }, () => true);

        private BaseCommand serviceSelectionChanged = null;
        public BaseCommand ServiceSelectionChanged => serviceSelectionChanged ??= new BaseCommand(x =>
        {
            SelectedPrice = SelectedService.Price.ToString();
        }, () => true);

        private BaseCommand exitCommand = null;
        public BaseCommand ExitCommand => exitCommand ??= new BaseCommand(x =>
        {
            CloseWindow.Invoke();
        }, () => true);
    }
}
