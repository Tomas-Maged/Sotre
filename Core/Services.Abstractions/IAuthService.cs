using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthService
    {
      Task<UserResutDto>  LoginAsync(LoginDto loginDto);
      Task<UserResutDto>  RegisterAsync(RegisterDto registerDto);


    }
}
