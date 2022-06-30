using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    [Table("Currencies")]
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public List<Loan> Loans { get; set; }

    }
}
