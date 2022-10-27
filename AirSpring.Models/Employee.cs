using System;
using System.Collections.Generic;

namespace AirSpring.DAL.DataContext
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string EmployeeZip { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
