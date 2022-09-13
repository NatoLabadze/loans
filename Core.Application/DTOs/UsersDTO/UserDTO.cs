using Core.Application.DTOs.UserDTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.UsersDTO
{
    public class UserDTO : LoginDTO
    {
        public string PasswordConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}
