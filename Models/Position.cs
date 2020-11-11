using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
