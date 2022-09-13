using AutoMapper;
using Core.Application.DTOs.LoansDTO;
using Core.Application.DTOs.UsersDTO;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.AutoMapperProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateLoansMapping();
            CreateUsersMapping();
        }
        private void CreateLoansMapping()
        {
            CreateMap<Loan, GetLoansDTO>()
                .ForMember(u => u.CurrencyName, opt => opt.MapFrom(ur => ur.Currency.Name))
                .ForMember(u => u.LoanTypeName, opt => opt.MapFrom(ur => ur.LoanType.Name))
                .ForMember(u => u.LoanStatusName, opt => opt.MapFrom(ur => ur.Status.Name));

            CreateMap<AddEditLoanDTO, Loan>();
      

        }
        private void CreateUsersMapping()
        {
          
            CreateMap<UserDTO, User>();

        }
    }
}
