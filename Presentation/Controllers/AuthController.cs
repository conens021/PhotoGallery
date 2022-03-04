﻿using BLL.Mappers.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        public AuthController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager) {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("/auth")]
        public ActionResult UserAuthenticate([FromBody] UserAuthenticate user) {
            UserAuthorize userFromDb = _userService.GetByUsernameOrEmailAndPassword(user.UsernameOrEmail, user.Password);
            return Ok(new UserSession() { User = userFromDb, Jwt = _jwtAuthenticationManager.Authenticate(userFromDb) });
        }
    }
}
