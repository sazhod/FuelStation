using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class EmployeeViewModel
    {
        private Employee employee;

        public EmployeeViewModel(Employee employee) => this.employee = employee;

        private int Id { get => employee.Id; }

        public string Lastname
        {
            get => employee.Lastname;
            set
            {
                employee.Lastname = value;
                SaveChanges();
            }
        }
        public string Firsname { 
            get => employee.Firsname; 
            set
            {
                employee.Lastname = value;
                SaveChanges();
            }
        }
        public string Patronymic { 
            get => employee.Patronymic; 
            set
            {
                employee.Lastname = value;
                SaveChanges();
            }
        }
        public string Login
        {
            get => employee.Login;
            set
            {
                employee.Login = value;
                SaveChanges();
            }
        }
        public string Password
        {
            get => employee.Password;
            set
            {
                employee.Password = value;
                SaveChanges();
            }
        }

        private int Idrole { get => employee.Idrole; }
        public string _Role { get => employee.IdroleNavigation.Type; }
        public List<Role> AllRole { get => EfCoreDbContext.Instance.Roles.ToList(); }
        public Role SelectedRole { get => AllRole.Single(r => r.Id == employee.Idrole); }

        private void SaveChanges()
        {
            EfCoreDbContext.Instance.Employees.Update(employee);
            EfCoreDbContext.Instance.SaveChanges();
        }
    }
}
