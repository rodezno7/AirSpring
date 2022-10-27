using AirSpring.DAL.Interface;
using AirSpring.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSpring.DAL.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int CreateEmployee(NewEmployeeDTO newEmployee)
        {
            int result = 0;
            try
            {
                // Connetion Details
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "sp_CreateNewEmployee"; // Stored Procedure used

                // Sending parameters
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmployeeLastName", newEmployee.EmployeeLastName);
                sqlCommand.Parameters.AddWithValue("@EmployeeFirstName", newEmployee.EmployeeFirstName);
                sqlCommand.Parameters.AddWithValue("@EmployeePhone", newEmployee.EmployeePhone);
                sqlCommand.Parameters.AddWithValue("@EmployeeZip", newEmployee.EmployeeZip);
                sqlCommand.Parameters.AddWithValue("@HireDate", DateTime.ParseExact(newEmployee.HireDate.ToString(), "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
                result = sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public int DeleteEmployee(DeleteEmployeeDTO deleteEmployee)
        {
            int result = 0;
            try
            {
                // Connetion Details
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "sp_DeleteEmployee"; // Stored Procedure used

                // Sending Parameters
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@employeeId", deleteEmployee.employeeId);
                result = sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public List<EmployeeDTO> GetAllEmployees()
        {
            List<EmployeeDTO> _list = new List<EmployeeDTO>();
            try
            {
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "sp_GetAllEmployees"; // Stored Procedure used
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                // Reading results
                SqlDataReader _reader = sqlCommand.ExecuteReader();
                var employee = new EmployeeDTO();
                while (_reader.Read())
                {
                    employee = new EmployeeDTO();
                    employee.EmployeeId = (int)_reader.GetInt32(0);
                    employee.EmployeeFirstName = _reader.GetString(1);
                    employee.EmployeeLastName = _reader.GetString(2);
                    employee.EmployeePhone = _reader.GetString(3);
                    employee.EmployeeZip = _reader.GetString(4);
                    employee.HireDate = _reader.GetDateTime(5).ToString("MM/dd/yyyy");
                    _list.Add(employee);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                _list = new List<EmployeeDTO>();
            }
            return _list;
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            EmployeeDTO _employee = new EmployeeDTO();
            try
            {
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "sp_GetEmployeeById"; // Stored Procedure used
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@employeeId", id);

                // Reading results
                SqlDataReader _reader = sqlCommand.ExecuteReader();
                var employee = new EmployeeDTO();
                while (_reader.Read())
                {
                    employee = new EmployeeDTO();
                    employee.EmployeeId = (int)_reader.GetInt32(0);
                    employee.EmployeeFirstName = _reader.GetString(1);
                    employee.EmployeeLastName = _reader.GetString(2);
                    employee.EmployeePhone = _reader.GetString(3);
                    employee.EmployeeZip = _reader.GetString(4);
                    employee.HireDate = _reader.GetDateTime(5).ToString("MM/dd/yyyy");
                }
                _employee = employee;
                connection.Close();
            }
            catch (Exception ex)
            {
                _employee = new EmployeeDTO();
            }
            return _employee;
        }

        public EmployeeDTO GetEmployeeByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(EmployeeDTO newEmployee)
        {
            int result = 0;
            try
            {
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "sp_UpdateEmployee"; // Stored Procedure used

                // Sending parameters
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmployeeId", newEmployee.EmployeeId);
                sqlCommand.Parameters.AddWithValue("@EmployeeLastName", newEmployee.EmployeeLastName);
                sqlCommand.Parameters.AddWithValue("@EmployeeFirstName", newEmployee.EmployeeFirstName);
                sqlCommand.Parameters.AddWithValue("@EmployeePhone", newEmployee.EmployeePhone);
                sqlCommand.Parameters.AddWithValue("@EmployeeZip", newEmployee.EmployeeZip);
                sqlCommand.Parameters.AddWithValue("@HireDate", DateTime.ParseExact(newEmployee.HireDate.ToString(), "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
                result = sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
    }
}
