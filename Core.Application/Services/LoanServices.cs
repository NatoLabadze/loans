using AutoMapper;
using Core.Application.DTOs.LoansDTO;
using Core.Application.Interfaces.Repository;
using Core.Application.Requests;
using Core.Domain;
using LoansAPI.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Core.Application.Services
{
    public class LoanServices
    {
        private readonly ILoansRepository loansRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;

        public LoanServices(ILoansRepository loansRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository
)
        {
            this.loansRepository = loansRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<GetLoansDTO>> GetAll(PageRequest pageRequest)
        {
            var userId = int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var allLoans = await loansRepository.GetAll(pageRequest, y => y.user.Id == userId, x => x.LoanType, x => x.Status, x => x.Currency);

            return mapper.Map<List<GetLoansDTO>>(allLoans);
        }

        public async Task UpdateLoans(int id, AddEditLoanDTO updateloantDTO)
        {

            var userId = int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var loan = await loansRepository.GetById(id);
        
            if (loan.UserId != userId)
            {
                throw new AppException("Not allowed");
            }

            loan.Amount = updateloantDTO.Amount;
            loan.CurrencyId = updateloantDTO.CurrencyId;
            loan.LoanTypeId = updateloantDTO.LoanTypeId;
            loan.Period = updateloantDTO.Period;

            var status = await loansRepository.GetById(id);

            if (status.StatusId == 3 || status.StatusId == 4)
            {

                throw new AppException("მსგავსი სტატუსის ჩანაწერზე არ შეგიძლიათ ცვლილებები");

            }
            else
            {
                await loansRepository.Update(id, loan);
            }

        }

        public async Task AddLoan(AddEditLoanDTO addLoanDTO)
        {
            var userId = int.Parse(this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await loansRepository.Add(new Loan
            {
                Amount = addLoanDTO.Amount,
                CurrencyId = addLoanDTO.CurrencyId,
                LoanTypeId = addLoanDTO.LoanTypeId,
                Period = addLoanDTO.Period,
                UserId = userId,
                StatusId = (int) Enums.Statuses.Sended
            });
        }

    }
}


