using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class FuelViewModel
    {
        public Fuel fuel;

        public FuelViewModel(Fuel fuel) => this.fuel = fuel;

        public int Id 
        { 
            get => fuel.Id; 
        }
        public string Name
        {
            get => fuel.Name;
        }
        public int Idmeasure
        {
            get => fuel.IdmeasureNavigation.Id;
        }
        public int Idvendor
        {
            get => fuel.IdvendorNavigation.Id;
        }
        public decimal CostPerUnit
        {
            get => fuel.CostPerUnit;
        }
    }
}
