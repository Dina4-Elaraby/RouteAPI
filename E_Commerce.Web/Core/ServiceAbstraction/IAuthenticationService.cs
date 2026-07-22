using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_DTOs_.IdentityDTOs;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //Login
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        //Register
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);

        //Check Email
        Task<bool> CheckEmailAsync(string email);

        //GetCurrentUser
        public Task<UserDTO> GetCurrentUserAsync(string email);

        //UpdateCurrentUserAddress
        Task<AddressDTO> UpdateCurrentUserAddressAsync(string email, AddressDTO addressDTO);

        //GetCurrentUserAddress
        public Task<AddressDTO> GetCurrentUserAdressAsync(string email);

    }
}
