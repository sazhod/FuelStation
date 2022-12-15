﻿using System;
using System.Collections.Generic;

namespace FuelStation.Models.Etities
{
    public partial class Fuel
    {
        public Fuel()
        {
            FuelSales = new HashSet<FuelSale>();
            FuelSupplies = new HashSet<FuelSupply>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public int Idvendor { get; set; }
        public int Idmeasure { get; set; }
        public decimal CostPerUnit { get; set; }

        public virtual Measure IdmeasureNavigation { get; set; } = null!;
        public virtual Vendor IdvendorNavigation { get; set; } = null!;
        public virtual ICollection<FuelSale> FuelSales { get; set; }
        public virtual ICollection<FuelSupply> FuelSupplies { get; set; }
    }
}
