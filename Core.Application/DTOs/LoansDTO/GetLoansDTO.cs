using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.DTOs.LoansDTO
{
   public class GetLoansDTO
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
        public string LoanTypeName { get; set; }
        public string LoanStatusName { get; set; }
    }
}
