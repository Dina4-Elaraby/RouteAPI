using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared_DTOs_.IdentityDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _config,IMapper mapper) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            //chcek email exist 
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user is null)  throw new UserNotFoundException(loginDTO.Email);

            //check pass
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user,loginDTO.Password);

            if (isPasswordCorrect)
                //return userdto
                return new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user,_userManager)
                };
            else
                throw new UnauthorizedException();
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            //mapping RegisterDTO -> ApplicationUser
            var user = new ApplicationUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            //create user[applicationuser]
            var res = await _userManager.CreateAsync(user, registerDTO.Password);

            //check if create user successfully
            if(res.Succeeded)
            {
                //return userdto
                return new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user,_userManager)

                };
            }
            else
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
            
        }
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
            
        }

        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user,_userManager)
            };
        }

        public async Task<AddressDTO> GetCurrentUserAdressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                      .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if (user.Address is not null) 
                //map from adddress to addressdto
                return mapper.Map<Address, AddressDTO>(source: user.Address); 
            else
                throw new AddressNotFoundException(user.UserName); 
        }
        public async Task<AddressDTO> UpdateCurrentUserAddressAsync(string email, AddressDTO addressDTO)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ??
               throw new UserNotFoundException(email);

            if (user.Address is not null) //update
            {
                user.Address.FName = addressDTO.FName;
                user.Address.LName = addressDTO.LName;
                user.Address.City = addressDTO.City;
                user.Address.Country = addressDTO.Country;
                user.Address.Street = addressDTO.Street;
            }
            else//add new address
            {
                user.Address = mapper.Map<AddressDTO, Address>(addressDTO);
                //update user
                await _userManager.UpdateAsync(user);
            }
            return mapper.Map<AddressDTO>(user.Address);

        }
        private async Task< string> CreateTokenAsync(ApplicationUser applicationUser, UserManager<ApplicationUser> _userManager)
        {
            var claims = new List<Claim>()
            {
                new Claim(type:ClaimTypes.Email,value:applicationUser.Email),
                new Claim(type:ClaimTypes.Name,value:applicationUser.UserName),
                new Claim(type:ClaimTypes.NameIdentifier,value:applicationUser.Id),
            };
            var roles = await _userManager.GetRolesAsync(applicationUser);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var securityKey = _config.GetSection("JwtOptions")["SecretKey"];
            var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var creds = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: _config["JwtOptions:Issuer"],
                audience: _config["JwtOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
