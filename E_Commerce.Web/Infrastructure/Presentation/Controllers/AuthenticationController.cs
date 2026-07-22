using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared_DTOs_.IdentityDTOs;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager ) :ApiBaseController
    {
        //Login
        [HttpPost("Login")]//baseurl/api/authentication/login
        public async Task<ActionResult<UserDTO>>Login(LoginDTO loginDTO)
        {
            var user = await serviceManager.authenticationService.LoginAsync(loginDTO);
            return Ok(user);
        }

        //register
        [HttpPost("Register")] //baseurl/api/authentication/register
        public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
        {
            var user = await serviceManager.authenticationService.RegisterAsync(registerDTO);
            return Ok(user);

        }

        //CheckEmail
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>>CheckEmail(string Email)
        {
          var res = await serviceManager.authenticationService.CheckEmailAsync(Email);
            return Ok(res);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDTO>>GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appUser = await serviceManager.authenticationService.GetCurrentUserAsync(email);
            return Ok(appUser);
        }

        [HttpGet("GetCurrentUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await serviceManager.authenticationService.GetCurrentUserAdressAsync(email);
            return Ok(address);
        }

        //updateAddress
        [HttpPut("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDTO>>UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await serviceManager.authenticationService.UpdateCurrentUserAddressAsync(email, addressDTO);
            return Ok(updatedAddress);
        }



    }
}
