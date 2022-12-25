using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class FuelSuppliesViewModel
    {
        public FuelSupply fuelSupply;

        public FuelSuppliesViewModel(FuelSupply fuelSupply)
        {
            this.fuelSupply = fuelSupply;
            SelectedVendor = fuelSupply.IdfuelNavigation.IdvendorNavigation;
            allFuels = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == SelectedVendor.Id).ToList();
        }

        public int Id { get; set; }
        public int IdReceivingEmployee { get; set; }
        public int IdBringingEmployee { get; set; }
        public string Datetime { get => fuelSupply.Datetime.ToString("dd.MM.yyyy HH:mm:ss"); }
        public int Idfuel { get; set; }
        public int Quantity { get; set; }

        public List<Employee> AllEmployees { get => EfCoreDbContext.Instance.Employees.ToList(); }

        public int ReceivingEmployeeId
        {
            get => fuelSupply.IdReceivingEmployeeNavigation.Id;
            set
            {
                fuelSupply.IdReceivingEmployeeNavigation = EfCoreDbContext.Instance.Employees.Single(e => e.Id == value);
                SaveChanges();
            }
        }

        public int BringingEmployeeId
        {
            get => fuelSupply.IdBringingEmployeeNavigation.Id;
            set
            {
                fuelSupply.IdBringingEmployeeNavigation = EfCoreDbContext.Instance.Employees.Single(e => e.Id == value);
                SaveChanges();
            }
        }

        public List<Fuel> AllFuels { get => allFuels; set => allFuels = value; }
        private List<Fuel> allFuels;


        public int FuelId
        {
            get => fuelSupply.IdfuelNavigation.Id;
            set
            {
                fuelSupply.IdfuelNavigation = EfCoreDbContext.Instance.Fuels.Single(f => f.Id == value);
                SaveChanges();
            }
        }

        public List<Vendor> AllVenders { get => EfCoreDbContext.Instance.Vendors.ToList(); }

        public int VendorId
        {
            get => SelectedVendor.Id;
            set
            {
                //fuelSupply.IdfuelNavigation.IdvendorNavigation = EfCoreDbContext.Instance.Vendors.Single(v => v.Id == value);
                SelectedVendor = EfCoreDbContext.Instance.Vendors.Single(v => v.Id == value);
                AllFuels = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == SelectedVendor.Id).ToList();
            }
        }

        public Vendor SelectedVendor { get; set; }

        private void SaveChanges()
        {
            EfCoreDbContext.Instance.FuelSupplies.Update(fuelSupply);
            EfCoreDbContext.Instance.SaveChanges();
        }
    }
}
