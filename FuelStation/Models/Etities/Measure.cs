using System;
using System.Collections.Generic;

namespace FuelStation.Models.Etities
{
    public partial class Measure
    {
        public Measure()
        {
            Fuels = new HashSet<Fuel>();
        }

        public int Id { get; set; }
        public string Measure1 { get; set; } = null!;

        public virtual ICollection<Fuel> Fuels { get; set; }
    }
}
