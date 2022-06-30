using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public bool IsBlocked { get; set; } = false;

        public int RoleId { get; set; } = 2;
        public List<Loan>? Loans { get; set; }
    }
}
