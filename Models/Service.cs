using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Service
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public int IsLaser { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
