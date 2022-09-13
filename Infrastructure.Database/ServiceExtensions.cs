using Core.Application.Interfaces.Repository;
using Infrastructure.Database.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database
{
    public static class ServiceExtensions
    {
        public static void AddDbLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoanDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("LoanDBConnection")));
            services.AddScoped<ILoansRepository, LoanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ILoantypeRepository,LoanTypeRepository>();


        }
    }
}
