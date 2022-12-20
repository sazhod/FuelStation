using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class RoleViewModel
    {
        public Role role { get; set; }

        public RoleViewModel(Role role) => this.role = role;
      
        public int Id { get => role.Id; }
        public string Type 
        { 
            get => role.Type; set
            {
                role.Type = value;
                SaveChanges();
            }
        }
        public int Salary 
        { 
            get => role.Salary; 
            set
            {
                role.Salary = value;
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            EfCoreDbContext.Instance.Roles.Update(role);
            EfCoreDbContext.Instance.SaveChanges();
        }
    }
}
