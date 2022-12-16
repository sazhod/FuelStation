using System;
using System.Collections.Generic;

namespace FuelStation.Models.Entities
{
    public partial class FuelSale
    {
        public int Id { get; set; }
        public int Idemployee { get; set; }
        public int Idfuel { get; set; }
        public DateTime Datetime { get; set; }
        public decimal Quantity { get; set; }
        public decimal Totalcost { get; set; }

        public virtual Employee IdemployeeNavigation { get; set; } = null!;
        public virtual Fuel IdfuelNavigation { get; set; } = null!;
    }
}
