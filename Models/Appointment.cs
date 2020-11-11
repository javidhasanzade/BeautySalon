using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Appointment
    {
        public Appointment()
        {
            Earnings = new HashSet<Earning>();
            LaserEarnings = new HashSet<LaserEarning>();
            StaffEarnings = new HashSet<StaffEarning>();
        }

        public int Id { get; set; }
        public int BranchId { get; set; }
        public int ClientId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public DateTime? Checkout { get; set; }

        
        public virtual Branch Branch { get; set; }
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        public virtual Employee Staff { get; set; }
        public virtual ICollection<Earning> Earnings { get; set; }
        public virtual ICollection<LaserEarning> LaserEarnings { get; set; }
        public virtual ICollection<StaffEarning> StaffEarnings { get; set; }

    }
}
