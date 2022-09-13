using Core.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Application
{
    public static class ServiceExtension
    {
        public static void AddAppLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<LoanServices>();
            services.AddScoped<UserServices>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMvc().AddFluentValidation();

        }
    }
}
