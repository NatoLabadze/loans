using AutoMapper;
using Core.Application.DTOs;
using Core.Application.DTOs.UsersDTO;
using Core.Application.Interfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Core.Application.Services
{
    public class UserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;


        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;

        }
        public async Task AddUser(UserDTO userDTO)
        {
            userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            var user = mapper.Map<User>(userDTO);
            await userRepository.Add(user);
        }
        public async Task<User> ValidateUser(string UserName, string Password)
        {
            return await userRepository.ValidateUser(UserName, Password);

        }
      
    }
}


