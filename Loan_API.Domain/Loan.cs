using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanPeriodTime { get; set; }
        public double Amount { get; set; }

        public int CurrencyId { get; set; }
        public int LoanTypeId { get; set; }

        public int UserId { get; set; }
        public int LoanStatusId { get; set; } = 1;
    }
}
