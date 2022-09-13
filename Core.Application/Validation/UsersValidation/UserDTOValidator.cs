using Core.Application.DTOs.UsersDTO;
using Core.Application.Interfaces.Repository;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Core.Application.Validation.UsersValidation
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {


        public UserDTOValidator(IUserRepository userRepository)
        {


            RuleFor(x => x.PrivateNumber).NotEmpty().WithMessage("აუცილებელი ველი");

            RuleFor(x => x.UserName).MustAsync(async (username, cancellation) =>
            {
                bool exists = await userRepository.ExistedUser(username);
                return !exists;
            }).WithMessage("მითითებული UserName უკვე არსებობს");

            RuleFor(x => x).Must(x => x.Password == x.PasswordConfirmed)
            .WithMessage(" პაროლები არ ემთხვევა ერთმანეთს");


            RuleFor(x => x.FirstName).NotEmpty().WithMessage("აუცილებელი ველი")
           .Must(CheckText).WithMessage("უნდა მოიცავდეს მხოლოდ ქართულ ან მხოლოდ ლათინურ ასოებს");



            RuleFor(x => x.LastName).NotEmpty().WithMessage("აუცილებელი ველი")
            .Must(CheckText).WithMessage("უნდა მოიცავდეს მხოლოდ ქართულ ან მხოლოდ ლათინურ ასოებს");
        }

        private bool CheckText(string text)
        {
            Regex r = new Regex("^[ა-ჰ]*$|^[a-zA-Z]*$");
            return r.IsMatch(text);

        }









    }
}
