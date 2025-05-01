using Domian.Exceptions;
using Domian.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<UserResutDto> LoginAsync(LoginDto loginDto)
        {
         var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User is null) throw new UnAuthorizedException() ;
            var Flag = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!Flag) throw new UnAuthorizedException();
            return new UserResutDto() 
            {
                DisplayName = User.DisplyName,
                Email = User.Email,
                Token = "Token",
            };

        }

        public async Task<UserResutDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplyName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
          var result = await userManager.CreateAsync(user,registerDto.Password);
            if (result.Succeeded)
            {
                var errors = result.Errors.Select(errors => errors.Description);
                throw new ValidationException(errors);
            }
            return new UserResutDto()
            {
                DisplayName = user.DisplyName,
                Email = user.Email,
                Token = "Token",
            };


        }
    }
}
