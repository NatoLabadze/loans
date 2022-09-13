using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.DTOs.LoansDTO
{
   public class AddEditLoanDTO
    {
        public int LoanTypeId { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public int Period { get; set; }

    }
}
