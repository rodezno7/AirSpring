using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSpring.Models.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string EmployeeZip { get; set; } = null!;
        public string? HireDate { get; set; }
    }

    public class NewEmployeeDTO
    {
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string EmployeeZip { get; set; } = null!;
        public string HireDate { get; set; }
    }

    public class DeleteEmployeeDTO
    {
        public int employeeId { get; set; }
    }
}
