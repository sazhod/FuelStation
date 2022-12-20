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
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();
            FillingDataGrid();
        }

        public void FillingDataGrid()
        {
            EmployeeDatagrid.ItemsSource = employeeViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            var result = addEmployee.ShowDialog();
            FillingDataGrid();
        }
    }
}
