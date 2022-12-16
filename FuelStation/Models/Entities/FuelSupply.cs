using System;
using System.Collections.Generic;

namespace FuelStation.Models.Entities
{
    public partial class FuelSupply
    {
        public int Id { get; set; }
        public int IdReceivingEmployee { get; set; }
        public int IdBringingEmployee { get; set; }
        public DateTime Datetime { get; set; }
        public int Idfuel { get; set; }
        public int Quantity { get; set; }

        public virtual Employee IdBringingEmployeeNavigation { get; set; } = null!;
        public virtual Employee IdReceivingEmployeeNavigation { get; set; } = null!;
        public virtual Fuel IdfuelNavigation { get; set; } = null!;
    }
}
