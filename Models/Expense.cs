using System;
using System.Collections.Generic;
using System.Text;

namespace BeautySalon.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
