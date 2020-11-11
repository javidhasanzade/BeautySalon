using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Branch
    {
        public Branch()
        {
            Appointments = new HashSet<Appointment>();
            Clients = new HashSet<Client>();
            Earnings = new HashSet<Earning>();
            Employees = new HashSet<Employee>();
            Expenses = new HashSet<Expense>();
            LaserEarnings = new HashSet<LaserEarning>();
            StaffEarnings = new HashSet<StaffEarning>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Earning> Earnings { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<LaserEarning> LaserEarnings { get; set; }
        public virtual ICollection<StaffEarning> StaffEarnings { get; set; }

    }
}
