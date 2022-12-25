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
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        private List<EmployeeViewModel> receivingEmployeeViewModel
        {
            get
            {
                var tempEmployees = EfCoreDbContext.Instance.Employees.Where(e => e.IdroleNavigation.Id == 4).ToList();
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                for (int i = 0; i < tempEmployees.Count; i++)
                {
                    employees.Add(new EmployeeViewModel(tempEmployees[i]));
                }
                return employees;
            }
        }

        private List<EmployeeViewModel> bringingEmployeeViewModel
        {
            get
            {
                var tempEmployees = EfCoreDbContext.Instance.Employees.Where(e => e.IdroleNavigation.Id == 2).ToList();
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

        private List<FuelViewModel> fuelViewModel
        {
            get
            {
                var tempFuel = EfCoreDbContext.Instance.Fuels.ToList();
                List<FuelViewModel> fuels = new List<FuelViewModel>();
                for (int i = 0; i < tempFuel.Count; i++)
                {
                    fuels.Add(new FuelViewModel(tempFuel[i]));
                }
                return fuels;
            }
        }

        private List<FuelSuppliesViewModel> fuelSuppliesViewModels
        {
            get
            {
                var tempFuelSupplies = EfCoreDbContext.Instance.FuelSupplies.ToList();
                List<FuelSuppliesViewModel> fuelSupplies = new List<FuelSuppliesViewModel>();
                for (int i = 0; i < tempFuelSupplies.Count; i++)
                {
                    fuelSupplies.Add(new FuelSuppliesViewModel(tempFuelSupplies[i]));
                }
                return fuelSupplies;
            }
        }

        public ManagerMainWindow()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            ReceivingEmployeeComboBox.ItemsSource = receivingEmployeeViewModel;
            BringingEmployeeComboBox.ItemsSource = bringingEmployeeViewModel;
            VendorComboBox.ItemsSource = vendorViewModel;
            FuelSuppliesDatagrid.ItemsSource = fuelSuppliesViewModels;
        }

        private bool AddFuelSuppliesValidate()
        {
            string errorMessage = "";
            if (QuantityTextBox.Text.IsNullOrEmpty() || BringingEmployeeComboBox.SelectedIndex == -1 || ReceivingEmployeeComboBox.SelectedIndex == -1 
                || VendorComboBox.SelectedIndex == -1 || FuelComboBox.SelectedIndex == -1)
                errorMessage = "Заполните все данные!";
            else if (Convert.ToInt32(QuantityTextBox.Text) == 0)
                errorMessage = "Количество не может быть = 0";

            if (!errorMessage.IsNullOrEmpty())
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return false;
            }
            return true;
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

        private void VendorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                Vendor vendor = ((VendorViewModel)((ComboBox)sender).SelectedItem).vendor;
                FuelComboBox.ItemsSource = EfCoreDbContext.Instance.Fuels.Where(f => f.IdvendorNavigation.Id == vendor.Id).ToList();
            }
        }

        private void AddFuelSuppliesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AddFuelSuppliesValidate())
                return;
            Employee receivingEmployee = ((EmployeeViewModel)ReceivingEmployeeComboBox.SelectedItem).employee;
            Employee bringingEmployee = ((EmployeeViewModel)BringingEmployeeComboBox.SelectedItem).employee;
            Fuel fuel = ((Fuel)FuelComboBox.SelectedItem);

            FuelSupply fuelSupply = new FuelSupply();

            fuelSupply.IdReceivingEmployeeNavigation = receivingEmployee;
            fuelSupply.IdBringingEmployeeNavigation = bringingEmployee;
            fuelSupply.IdfuelNavigation = fuel;
            fuelSupply.Datetime = DateTime.Now;
            fuelSupply.Quantity = Convert.ToInt32(QuantityTextBox.Text);

            EfCoreDbContext.Instance.FuelSupplies.Add(fuelSupply);
            EfCoreDbContext.Instance.SaveChanges();

            CustomMessageBox customMessageBox = new CustomMessageBox("Новый приход товара успешно зарегистрирован!");
            customMessageBox.ShowDialog();
            ClearData();
            FillComboBox();
        }

        private void ClearData()
        {
            ReceivingEmployeeComboBox.SelectedIndex = -1;
            BringingEmployeeComboBox.SelectedIndex = -1;
            VendorComboBox.SelectedIndex = -1;
            FuelComboBox.SelectedIndex = -1;
            QuantityTextBox.Text = "";
        }

        private void DeleteFuelSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDeleteMessageBox confirmationDeleteMessageBox = new ConfirmationDeleteMessageBox();
            if (confirmationDeleteMessageBox.ShowDialog() == true)
            {
                //Role removingRole = ((RoleViewModel)((Button)sender).DataContext).role;
                var removingFuelSupply = ((FuelSuppliesViewModel)((Button)sender).DataContext).fuelSupply;

                EfCoreDbContext.Instance.FuelSupplies.Remove(removingFuelSupply);
                EfCoreDbContext.Instance.SaveChanges();
                FillComboBox();
            }
        }
    }
}
