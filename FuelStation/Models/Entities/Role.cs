using System;
using System.Collections.Generic;

namespace FuelStation.Models.Entities
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int Salary { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
