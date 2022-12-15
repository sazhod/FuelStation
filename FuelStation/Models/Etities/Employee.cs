﻿using System;
using System.Collections.Generic;

namespace FuelStation.Models.Etities
{
    public partial class Employee
    {
        public Employee()
        {
            FuelSales = new HashSet<FuelSale>();
            FuelSupplyIdBringingEmployeeNavigations = new HashSet<FuelSupply>();
            FuelSupplyIdReceivingEmployeeNavigations = new HashSet<FuelSupply>();
        }

        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public int Idrole { get; set; }

        public virtual Role IdroleNavigation { get; set; } = null!;
        public virtual ICollection<FuelSale> FuelSales { get; set; }
        public virtual ICollection<FuelSupply> FuelSupplyIdBringingEmployeeNavigations { get; set; }
        public virtual ICollection<FuelSupply> FuelSupplyIdReceivingEmployeeNavigations { get; set; }
    }
}
