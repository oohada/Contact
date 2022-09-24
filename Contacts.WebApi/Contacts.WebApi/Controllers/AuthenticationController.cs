﻿using System.Threading.Tasks;
using Contacts.Core;
using Contacts.Core.CustomExceptions;
using Contacts.DB;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public AuthenticationController(IUserServices userServices)
        {
            _userServices = userServices; 
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                var result = await _userServices.SignUp(user);
                return Created("", result);
            }
            catch (UsernameAlreadyExistsException e)
            {

                return StatusCode(409, e.Message); 
            }
           
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
        {
            try
            {
                var result = await _userServices.SignIn(user);
                return Ok(result); 
            }
            catch (InvalidUsernamePasswordException e)
            {

                return StatusCode(401, e.Message);
            }
        }

    }
}
