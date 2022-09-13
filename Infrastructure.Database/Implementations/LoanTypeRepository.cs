using Core.Application.Interfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database.Implementations
{
    public class LoanTypeRepository : Repository<LoanType>, ILoantypeRepository
    {
        public LoanTypeRepository(LoanDbContext context) : base(context)
        {

        }


    }
}