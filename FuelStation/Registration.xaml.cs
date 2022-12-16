using FuelStation.Models.Entities;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = EfCoreDbContext.Instance.Employees.Where(e => e.Login == LoginTextBox.Text).ToList();
            if(employees.Count > 0 )
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Пользователь с таким логином уже существует.");
                customMessageBox.ShowDialog();
            }
            else
            {
                Employee newEmployee = new Employee();
                newEmployee.Login = LoginTextBox.Text;
                newEmployee.Password = PasswordBox.Password;
                newEmployee.Login = LoginTextBox.Text;

                EfCoreDbContext.Instance.Employees.Add(newEmployee);
                EfCoreDbContext.Instance.SaveChanges();
            }
            this.Close();
        }
    }
}
