using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Employee
    {
        public Employee()
        {
            Appointments = new HashSet<Appointment>();
            StaffEarnings = new HashSet<StaffEarning>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname { get => Name + " " + Surname; }
        public int PositionId { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<StaffEarning> StaffEarnings { get; set; }
    }
}
