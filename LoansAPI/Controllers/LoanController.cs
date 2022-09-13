using Core.Application.DTOs.LoansDTO;
using Core.Application.Requests;
using Core.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoansAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly LoanServices loanservice;

        public LoanController(LoanServices loanservice)
        {
            this.loanservice = loanservice;
        }
        // GET: api/<LoanController>

        [HttpGet]
        public async Task<IActionResult> GetAllLoans([FromQuery] PageRequest pageRequest)
        {

            return Ok(await loanservice.GetAll(pageRequest));
        }

        // POST api/<LoanController>

        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddEditLoanDTO addLoanDTO)
        {
            await loanservice.AddLoan(addLoanDTO);
            return Ok();
        }

     
        // PUT api/<LoanController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddEditLoanDTO updateLoanDTO)
        { 
            await loanservice.UpdateLoans(id, updateLoanDTO);
            return Ok();
        }

    }
}
