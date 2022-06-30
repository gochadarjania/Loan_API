using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Application.RoleUser.Queries.ViewModel
{
    public class LoanViewModel
    {
        public int Id { get; set; }

        public DateTime LoanPeriodTime { get; set; }
        public double Amount { get; set; }

        public string Currency { get; set; }
        public string LoanType { get; set; }
        public string LoanLoanStatus { get; set; }
        public int UserId { get; set; }
    }
}
