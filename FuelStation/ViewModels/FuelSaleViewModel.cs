using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    internal class FuelSaleViewModel
    {
        public FuelSale fuelSale;

        public FuelSaleViewModel(FuelSale fuelSale)
        {
            this.fuelSale = fuelSale;
            SelectedVendor = fuelSale.IdfuelNavigation.IdvendorNavigation;
            allFuels = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == SelectedVendor.Id).ToList();
        }

        public int Id { get => fuelSale.Id; }
        public int Idemployee { get; set; }
        public int Idfuel { get; set; }
        public string Datetime { get => fuelSale.Datetime.ToString("dd.MM.yyyy HH:mm:ss"); }
        public decimal Quantity 
        { 
            get => fuelSale.Quantity;
            set
            {
                fuelSale.Quantity = value;
                SaveChanges();
            }
        }
        public double Totalcost 
        {
            get => fuelSale.Totalcost;
            set
            {
                fuelSale.Totalcost = value;
                SaveChanges();
            }
        }

        public List<Employee> AllEmployees
        {
            get => EfCoreDbContext.Instance.Employees.ToList();
        }

        public int EmployeeId
        {
            get => fuelSale.IdemployeeNavigation.Id;
            set
            {
                fuelSale.IdemployeeNavigation = EfCoreDbContext.Instance.Employees.Single(e => e.Id == value);
                SaveChanges();
            }
        }

        public List<Fuel> AllFuels { get => allFuels; set => allFuels = value; }
        private List<Fuel> allFuels;


        public int FuelId
        {
            get => fuelSale.IdfuelNavigation.Id;
            set
            {
                fuelSale.IdfuelNavigation = EfCoreDbContext.Instance.Fuels.Single(f => f.Id == value);
                SaveChanges();
            }
        }

        public List<Vendor> AllVenders { get => EfCoreDbContext.Instance.Vendors.ToList(); }

        public int VendorId
        {
            get => SelectedVendor.Id;
            set
            {
                SelectedVendor = EfCoreDbContext.Instance.Vendors.Single(v => v.Id == value);
                AllFuels = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == SelectedVendor.Id).ToList();
            }
        }

        public Vendor SelectedVendor { get; set; }

        private void SaveChanges()
        {
            EfCoreDbContext.Instance.FuelSales.Update(fuelSale);
            EfCoreDbContext.Instance.SaveChanges();
        }
    }
}
