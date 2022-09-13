using Core.Application.DTOs.LoansDTO;
using Core.Application.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Application.Validation.LoansValidation
{
    public class AddLEditoanDTOValidator : AbstractValidator<AddEditLoanDTO> 
    {
        public AddLEditoanDTOValidator(ILoansRepository loanRepository,ILoantypeRepository loantypeRepository ,ICurrencyRepository currencyRepository)
        {
           

            RuleFor(x => x.Period).NotEmpty().WithMessage("აუცილებელი ველი");

            RuleFor(x => x.LoanTypeId).MustAsync(async (id, cancellation) =>
            {
                bool exists = await loantypeRepository.Existed(id);
                return exists;
            }).WithMessage("მითითებული სესხის ტიპი არ არსებობს");



            RuleFor(x => x.CurrencyId).MustAsync(async (id, cancellation) =>
            {
                bool exists = await currencyRepository.Existed(id);
                return exists;
            }).WithMessage("მითითებული ვალუტა არ არსებობს");
        }
            
    }
}
