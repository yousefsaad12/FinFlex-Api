using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.DTO.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {   
        private readonly UserManager<AppUser> _userManger;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManger = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registryDTO)
        {
            try 
            {   
                if(!ModelState.IsValid)
                    return BadRequest();

                var appUser = new AppUser
                {
                    UserName = registryDTO.UserName,
                    Email = registryDTO.Email
                };

                var createUser = await _userManger.CreateAsync(appUser, registryDTO.Password);

                if(createUser.Succeeded)
                {
                    var roleResult = await _userManger.AddToRoleAsync(appUser, "User");

                    if(roleResult.Succeeded)
                        return Ok
                        (
                            new NewUserDTO
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );

                    else
                        return StatusCode(500, roleResult.Errors);
                }

                else
                    return StatusCode(500, createUser.Errors);

            }

            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult>Login(LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.UserName.ToLower());

            if(user == null)
                return Unauthorized("Invalid username or password !");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Username is not found or password is not correct");

            return Ok(
                new NewUserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }
            );
            
        }

    }
}