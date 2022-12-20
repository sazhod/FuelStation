using FuelStation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.ViewModels
{
    class RoleViewModel
    {
        public Role role { get; set; }

        public RoleViewModel(Role role) => this.role = role;
      
        public int Id { get => role.Id; }
        public string Type { get => role.Type; }
        public int Salary { get => role.Salary; }
    }
}
