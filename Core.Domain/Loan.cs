using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public int Period { get; set; }
        public int LoanTypeId { get; set; }
        public int StatusId { get; set; }
        public LoanType LoanType { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public Currency Currency { get; set; }
    }
}
