using AirSpring.DAL.DataContext;
using AirSpring.DAL.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSpring.DAL.Repository
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Authentication for user and password
        public User LoginUser(User user)
        {
            User _user;
            try
            {
                string connectionString = _configuration.GetConnectionString("AirSpringSQL");
                SqlConnection connection = new SqlConnection(connectionString);
                string sqlquery = "SELECT Id, Name, Username FROM dbo.Users WHERE Username = @Username AND Password = @Password";

                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlquery, connection);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Username", user.Username);
                sqlCommand.Parameters.AddWithValue("@Password", Utils.Encrypt(user.Password));

                SqlDataReader _reader = sqlCommand.ExecuteReader();
                _user = new User();
                while (_reader.Read())
                {
                    _user.Id = _reader.GetInt64(0);
                    _user.Name = _reader.GetString(1);
                    _user.Username = _reader.GetString(2);
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                _user = new User();
            }

            return _user;
        }
    }
}
