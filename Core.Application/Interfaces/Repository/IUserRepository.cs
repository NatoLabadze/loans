using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> ValidateUser(string userName, string password);
        Task<bool> ExistedUser(string userName);
   
   
        
    }
}
