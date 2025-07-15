using Domian.Exceptions;
using Domian.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Services
{
    public class AuthService(UserManager<AppUser> userManager , IOptions<Jwtoption> options) : IAuthService
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
                Token = await GeneratejwtTokenAsync(User),
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
                Token = await GeneratejwtTokenAsync(user),
            };


        }

        private async Task<string> GeneratejwtTokenAsync(AppUser user)
        {
            //Header
            //payload
            //Signature
            var Jwtoption = options.Value;
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email, user.Email),

            };
          var Roles = await  userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecuretyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtoption.SecuretyKey));
            var Token = new JwtSecurityToken
                (
                    issuer: Jwtoption.issuer, 
                    audience: Jwtoption.Audience, 
                    claims: AuthClaims,  
                    expires: DateTime.UtcNow.AddDays(Jwtoption.DurationInDays), 
                    signingCredentials: new SigningCredentials
                    (SecuretyKey ,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
