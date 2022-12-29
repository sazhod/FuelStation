using Castle.Core.Internal;
using FuelStation.Models.Entities;
using FuelStation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FuelStation
{
    /// <summary>
    /// Логика взаимодействия для SalesmanMainWindow.xaml
    /// </summary>
    public partial class SalesmanMainWindow : Window
    {
        private List<EmployeeViewModel> salesEmployeeViewModel
        {
            get
            {
                var tempEmployees = EfCoreDbContext.Instance.Employees.Where(e => e.IdroleNavigation.Id == 3).ToList();
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                for (int i = 0; i < tempEmployees.Count; i++)
                {
                    employees.Add(new EmployeeViewModel(tempEmployees[i]));
                }
                return employees;
            }
        }

        private List<VendorViewModel> vendorViewModel
        {
            get
            {
                var tempVendor = EfCoreDbContext.Instance.Vendors.ToList();
                List<VendorViewModel> vendors = new List<VendorViewModel>();
                for (int i = 0; i < tempVendor.Count; i++)
                {
                    vendors.Add(new VendorViewModel(tempVendor[i]));
                }
                return vendors;
            }
        }


        private List<FuelSaleViewModel> fuelSaleViewModels
        {
            get
            {
                var tempFuelSale = EfCoreDbContext.Instance.FuelSales.ToList();
                List<FuelSaleViewModel> fuelSales = new List<FuelSaleViewModel>();
                for (int i = 0; i < tempFuelSale.Count; i++)
                {
                    fuelSales.Add(new FuelSaleViewModel(tempFuelSale[i]));
                }
                return fuelSales;
            }
        }

        public SalesmanMainWindow()
        {
            InitializeComponent();
            FillingFuelSaleData();
        }

        public void FillingFuelSaleData()
        {
            FuelSaleEmployeeComboBox.ItemsSource = salesEmployeeViewModel;
            FuelSaleVendorComboBox.ItemsSource = vendorViewModel;
            FuelSalesDatagrid.ItemsSource = fuelSaleViewModels;
        }

        private bool AddFuelSaleValidate()
        {
            string errorMessage = "";
            if (FuelSaleEmployeeComboBox.SelectedIndex == -1 || FuelSaleVendorComboBox.SelectedIndex == -1
                || FuelSaleFuelComboBox.SelectedIndex == -1 || FuelSaleQuantityTextBox.Text.IsNullOrEmpty())
                errorMessage = "Заполните все данные!";
            else if (Convert.ToInt32(FuelSaleQuantityTextBox.Text) == 0)
                errorMessage = "Количество не может быть = 0";

            if (!errorMessage.IsNullOrEmpty())
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void AddFuelSaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AddFuelSaleValidate())
                return;

            Employee salesEmployee = ((EmployeeViewModel)FuelSaleEmployeeComboBox.SelectedItem).employee;
            Fuel fuel = ((Fuel)FuelSaleFuelComboBox.SelectedItem);

            FuelSale fuelSale = new FuelSale();

            fuelSale.IdemployeeNavigation = salesEmployee;
            fuelSale.IdfuelNavigation = fuel;
            fuelSale.Datetime = DateTime.Now;
            fuelSale.Quantity = Convert.ToInt32(FuelSaleQuantityTextBox.Text);
            fuelSale.Totalcost = Convert.ToInt32(FuelSaleQuantityTextBox.Text) * Convert.ToDouble(fuel.CostPerUnit);

            EfCoreDbContext.Instance.FuelSales.Add(fuelSale);
            EfCoreDbContext.Instance.SaveChanges();

            CustomMessageBox customMessageBox = new CustomMessageBox("Новая продажа товара успешно зарегистрирована!");
            customMessageBox.ShowDialog();
            ClearData();
            FillingFuelSaleData();
        }

        private void ClearData()
        {
            FuelSaleEmployeeComboBox.SelectedIndex = -1;
            FuelSaleVendorComboBox.SelectedIndex = -1;
            FuelSaleFuelComboBox.SelectedIndex = -1;
            FuelSaleQuantityTextBox.Text = "";
        }

        private void FuelSaleVendorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                Vendor vendor = ((VendorViewModel)((ComboBox)sender).SelectedItem).vendor;
                FuelSaleFuelComboBox.ItemsSource = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == vendor.Id).ToList();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out var salary))
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void DeleteFuelSaleButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDeleteMessageBox confirmationDeleteMessageBox = new ConfirmationDeleteMessageBox();
            if (confirmationDeleteMessageBox.ShowDialog() == true)
            {
                var removingFuelSale= ((FuelSaleViewModel)((Button)sender).DataContext).fuelSale;

                EfCoreDbContext.Instance.FuelSales.Remove(removingFuelSale);
                EfCoreDbContext.Instance.SaveChanges();
                FillingFuelSaleData();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
