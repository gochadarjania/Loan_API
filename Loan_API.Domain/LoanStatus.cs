using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    [Table("LoanStatuses")]
    public class LoanStatus
    {
        public int Id { get; set; }
        public string LoanStatusName { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
