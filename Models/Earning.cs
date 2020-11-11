using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Earning
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int AppointmentId { get; set; }
        public int BranchId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
