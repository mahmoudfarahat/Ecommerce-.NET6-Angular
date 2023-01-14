﻿using AutoMapper;
using Ecom.BLL.Entities.Identity;
using Ecom.BLL.Interfaces;
using ecommerce.Dto;
using ecommerce.Errors;
using ecommerce.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IAppSession _appSession;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ITokenService tokenService , IMapper mapper , IAppSession appSession
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
           _tokenService = tokenService;
            _mapper = mapper;
            _appSession = appSession;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
         }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if(CheckEMailExistesAsync(register.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[]
                    {
                        "Email Address already in use"
                    }
                });
            }

            var user = new AppUser
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                    UserName = register.Email
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEMailExistesAsync([FromQuery] string email)
            => await _userManager.FindByEmailAsync(email) != null;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            { 
                Email = user.Email,
                 DisplayName = user.DisplayName,
            
                 Token = _appSession.AuthorizationToken,
           };
        }
    }  
}
