using System;
using System.Collections.Generic;

namespace FuelStation.Models.Entities
{
    public partial class Vendor
    {
        public Vendor()
        {
            Fuels = new HashSet<Fuel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Fuel> Fuels { get; set; }
    }
}
