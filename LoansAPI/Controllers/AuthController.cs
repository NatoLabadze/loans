using Core.Application.DTOs.UsersDTO;
using Core.Application.Interfaces.Repository;
using Core.Application.Services;
using LoansAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoansAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private readonly UserServices userServices;

        public AuthController(IUserRepository userRepository, UserServices userServices)
        {
            this.userRepository = userRepository;

            this.userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost("logIn")]


        public async Task<IActionResult> LogIn([FromQuery] string UserName, [FromQuery] string Password)
        {
         
            var user = await userServices.ValidateUser(UserName, Password); 


            if (user == null)
                return BadRequest("იუზერი ან პაროლი არასწორია");

            var token = JwtValidationExtensions.GenerateJwtToken(
                user.Id.ToString(),
                 user.UserName,
                user.FirstName,
                user.LastName
                );

            Response.Headers.Add("AccessToken", token);
            return Ok(user);


        }
        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            await userServices.AddUser(userDTO);
            return Ok();


        }

    }
}
