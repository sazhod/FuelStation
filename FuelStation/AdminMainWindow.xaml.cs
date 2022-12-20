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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        private List<EmployeeViewModel> employeeViewModel 
        { 
            get
            {
                var tempEmployees = EfCoreDbContext.Instance.Employees.ToList();
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                for (int i = 0; i < tempEmployees.Count; i++)
                {
                    employees.Add(new EmployeeViewModel(tempEmployees[i]));
                }
                return employees;
            }
        }

        private List<RoleViewModel> roleViewModels
        {
            get 
            {
                var tempRoles = EfCoreDbContext.Instance.Roles.ToList();
                List<RoleViewModel> roles = new List<RoleViewModel>();
                for (int i = 0; i < tempRoles.Count; i++)
                {
                    roles.Add(new RoleViewModel(tempRoles[i]));
                }
                return roles;
            }
        }

        public AdminMainWindow()
        {
            InitializeComponent();
            FillingDataGrid();
        }

        public void FillingDataGrid()
        {
            EmployeeDatagrid.ItemsSource = employeeViewModel;
            RoleDatagrid.ItemsSource = roleViewModels;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            var result = addEmployee.ShowDialog();
            FillingDataGrid();
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            AddRole addRole = new AddRole();
            var result = addRole.ShowDialog();
            FillingDataGrid();
        }

        private void DeleteRoleButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDeleteMessageBox confirmationDeleteMessageBox = new ConfirmationDeleteMessageBox();
            if (confirmationDeleteMessageBox.ShowDialog() == true)
            {
                Role removingRole = ((RoleViewModel)((Button)sender).DataContext).role;

                EfCoreDbContext.Instance.Roles.Remove(removingRole);
                EfCoreDbContext.Instance.SaveChanges();
                FillingDataGrid();
            }
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDeleteMessageBox confirmationDeleteMessageBox = new ConfirmationDeleteMessageBox();
            if (confirmationDeleteMessageBox.ShowDialog() == true)
            {
                Employee removingEmployee = ((EmployeeViewModel)((Button)sender).DataContext).employee;

                if (removingEmployee.IdroleNavigation.Id == 1)
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("Вы не можете удалить администратора! Данное действие можно выполнить только из БД!");
                    customMessageBox.ShowDialog();
                    return;
                }

                EfCoreDbContext.Instance.Employees.Remove(removingEmployee);
                EfCoreDbContext.Instance.SaveChanges();
                FillingDataGrid();
            }
        }
    }
}
