using Core.Application.Interfaces.Repository;
using Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Database.Implementations
{
    public class LoanRepository : Repository<Loan>, ILoansRepository
    {
        public LoanRepository(LoanDbContext context) : base(context)
        {
            
        }


    }
}

