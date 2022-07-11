using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using AuthenticateMicroservice.Model;
using AuthenticateMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticateMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthRepository authRepository;
        public AuthController(IAuthRepository _authRepository)
        {
            authRepository = _authRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Auth(UserCredentails creds)
        {
            System.Diagnostics.Debug.WriteLine(creds);
            IActionResult response = Unauthorized();
            var user = authRepository.AuthenticateUser(creds);
            if (user != null)
            {
                var tokenString = authRepository.GenerateJSONWebToken(user);
                return Ok(new { token = tokenString, sucess = true});
            }
            return response;
        }
    }
}