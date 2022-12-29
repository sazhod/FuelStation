using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class EmployeeViewModel
    {
        public Employee employee;

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
                employee.Firsname = value;
                SaveChanges();
            }
        }
        public string Patronymic { 
            get => employee.Patronymic; 
            set
            {
                employee.Patronymic = value;
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

        public string FullName
        {
            get => $"{employee.Firsname} {employee.Lastname} {employee.Patronymic}";
            
        }

        public List<Role> AllRoles 
        { 
            get => EfCoreDbContext.Instance.Roles.ToList(); 
        }

        public int RoleId
        {
            get => employee.IdroleNavigation.Id;
            set
            {
                employee.IdroleNavigation = EfCoreDbContext.Instance.Roles.Single(r => r.Id == value);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            EfCoreDbContext.Instance.Employees.Update(employee);
            EfCoreDbContext.Instance.SaveChanges();
        }
    }
}
