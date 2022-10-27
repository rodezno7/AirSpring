using AirSpring.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSpring.DAL.Interface
{
    // Employee Interface
    // All methods used to manage employees
    public interface IEmployee
    {
        public List<EmployeeDTO> GetAllEmployees();
        public int CreateEmployee(NewEmployeeDTO newEmployee);
        public int UpdateEmployee(EmployeeDTO newEmployee);
        public EmployeeDTO GetEmployeeByLastName(string lastName);
        public int DeleteEmployee(DeleteEmployeeDTO deleteEmployee);
        public EmployeeDTO GetEmployeeById(int id);
    }
}
