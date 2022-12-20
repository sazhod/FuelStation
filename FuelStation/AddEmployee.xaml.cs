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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private List<RoleViewModel> roleViewModel
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

        public AddEmployee()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = roleViewModel;
            RoleComboBox.SelectedIndex = 0;
        }

        private bool AddEmployeeValidate()
        {
            string errorMessage = "";
            if (LoginTextBox.Text.IsNullOrEmpty() || PasswordBox.Password.IsNullOrEmpty() || RepeatPasswordBox.Password.IsNullOrEmpty() || LastNameTextBox.Text.IsNullOrEmpty()
                || FirstNameTextBox.Text.IsNullOrEmpty() || PatronymicTextBox.Text.IsNullOrEmpty())
                errorMessage = "Заполните все данные!";
            else if (LoginTextBox.Text.Length < 5)
                errorMessage = "Введён слишком которокий лоигн!(Минимум 5 символов)";
            else if (!PasswordBox.Password.Equals(RepeatPasswordBox.Password))
                errorMessage = "Пароли не совпадают!";
            else if (EfCoreDbContext.Instance.Employees.Where(e => e.Login == LoginTextBox.Text).ToList().Count != 0)
            {
                errorMessage = "Пользователь с таким логином уже зарегистрирован";
            }

            if (!errorMessage.IsNullOrEmpty())
            {
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
                return false;
            }
            return true;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AddEmployeeValidate())
                return;

            Employee newEmployee = new Employee();
            newEmployee.Lastname = LastNameTextBox.Text;
            newEmployee.Firsname = FirstNameTextBox.Text;
            newEmployee.Patronymic = PatronymicTextBox.Text;
            newEmployee.Login = LoginTextBox.Text;
            newEmployee.Password = PasswordBox.Password;
            newEmployee.IdroleNavigation = ((RoleViewModel)RoleComboBox.SelectedItem).role;

            EfCoreDbContext.Instance.Employees.Add(newEmployee);
            EfCoreDbContext.Instance.SaveChanges();

            this.Close();


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
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
    }
}
