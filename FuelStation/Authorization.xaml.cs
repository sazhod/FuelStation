using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FuelStation.Models.Entities;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FuelStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            List<Employee> employee = EfCoreDbContext.Instance.Employees.Where(e => e.Login == login && e.Password == password).ToList();
            if (employee.Count != 1)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Проверьте правильность ввода данных.");
                customMessageBox.ShowDialog();
            }
            else if (employee.Count == 1 && employee[0].Login == login && employee[0].Password == password)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
