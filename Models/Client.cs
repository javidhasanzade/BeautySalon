using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Client
    {
        public Client()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname { get => Name + " " + Surname; }
        public string PhoneNumber { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
