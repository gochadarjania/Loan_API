using Loan_API.Domain;
using Loan_API_XUnit_Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Loan_API_XUnit_Tests.RoleUser
{
    internal class UserServices
    {
        List<User> _users;
        public UserServices()
        {
            DataDbConcext _dataDbConcext = new DataDbConcext();
            _users = _dataDbConcext.Users;
        }

        public string LoginUser(string userName, string password)
        {
            var user = _users.Find(x => x.UserName == userName);
            if (user is null || !BCryptNet.Verify(password, user.Password))
            {
                return "Username or password is incorrect";
            }
            return user.UserName;
        }

        public string RegistrationUser(User userNew)
        {
            var result = _users.Find(x => x.UserName == userNew.UserName);
            if (result != null)
            {
                return "is already taken";
            }
            userNew.Password = BCryptNet.HashPassword(userNew.Password);
            _users.Add(userNew);

            return userNew.UserName;
        }
    }
}
