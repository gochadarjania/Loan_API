using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    public class LoanType
    {
        public int Id { get; set; }
        public string LoanTypeName { get; set; }
        public List<Loan> Loans { get; set; }

    }
}
