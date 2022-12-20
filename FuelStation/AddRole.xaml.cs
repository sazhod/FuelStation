using Castle.Core.Internal;
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
    /// Логика взаимодействия для AddRole.xaml
    /// </summary>
    public partial class AddRole : Window
    {
        public AddRole()
        {
            InitializeComponent();
        }

        private bool AddEmployeeValidate()
        {
            string errorMessage = "";
            if (TypeTextBox.Text.IsNullOrEmpty() || SalaryTextBox.Text.IsNullOrEmpty())
                errorMessage = "Заполните все данные!";
            else if (TypeTextBox.Text.Length < 5)
                errorMessage = "Длинна должности слишком короткая!(Минимум 5 символов)";
            else if (EfCoreDbContext.Instance.Roles.Where(e => e.Type == TypeTextBox.Text).ToList().Count != 0)
            {
                errorMessage = "Такая должность уже существует!";
            }

            if (!errorMessage.IsNullOrEmpty())
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AddEmployeeValidate())
                return;
            Role newRole = new Role();
            newRole.Type = TypeTextBox.Text;
            newRole.Salary = Convert.ToInt32(SalaryTextBox.Text);
            EfCoreDbContext.Instance.Roles.Add(newRole);
            EfCoreDbContext.Instance.SaveChanges();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TypeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SalaryTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out var salary))
                e.Handled = true;
        }

        private void SalaryTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
