using Core.Application.Interfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database.Implementations
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(LoanDbContext context) : base(context)
        {

        }


    }
}