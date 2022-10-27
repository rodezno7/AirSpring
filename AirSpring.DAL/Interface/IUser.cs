using AirSpring.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSpring.DAL.Interface
{
    public interface IUser
    {
        public User LoginUser(User user);
    }
}
