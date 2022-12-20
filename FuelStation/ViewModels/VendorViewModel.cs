using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class VendorViewModel
    {

        public Vendor vendor;

        public VendorViewModel(Vendor vendor) => this.vendor = vendor;

        public int Id 
        {
            get => vendor.Id; 
        }
        public string Name 
        {
            get => vendor.Name;
        }
        public string Description 
        { 
            get => vendor.Description; 
        }
    }
}
