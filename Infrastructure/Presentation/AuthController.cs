﻿using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServicesManager servicesManager) : ControllerBase
    {

        [HttpPost("Login")]
        //Login
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await servicesManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await servicesManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
