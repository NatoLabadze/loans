
using Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoansAPI.Services
{

    public static class JwtValidationExtensions
    {

        private const string _securityKey = "24140739383344d89c5df8e06da16b17";
        /// <summary>
        /// ავთენთიფიკაციის პარამეტრების დამატება (ტოკენის ვალიდურობის შემოწმება)
        /// </summary>
        public static void AddJwtAuthenticationConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateActor = false,

                            ValidateLifetime = true,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey))
                        };
                    });
        }


        /// <summary>
        /// ავტორიზაციის პარამეტრების დამატება (მეთოდებზე დაშვების შემოწმება)
        /// </summary>
        public static void AddJwtAuthorizationConfigs(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();


                options.AddPolicy("TestPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "create:user"));
                });
            });
        }


        /// <summary>
        /// ტოკენის გენერირება
        /// </summary>
        public static string GenerateJwtToken(
            string userId,
            string loginName,
            string firstName,
            string lastName
   
            )
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim( ClaimTypes.NameIdentifier, userId),
                new Claim("loginName", loginName),
                new Claim("firstName", firstName),
                new Claim("lastName", lastName)
            };

        

            // ქმნის JWT ხელმოწერას
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),

                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
