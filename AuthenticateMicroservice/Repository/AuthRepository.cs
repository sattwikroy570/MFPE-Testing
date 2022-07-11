using AuthenticateMicroservice.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private IConfiguration _config;
        public AuthRepository(IConfiguration config)
        {
            _config = config;
        }
        public static List<UserCredentails> clientList = new List<UserCredentails>
        {
            new UserCredentails{UserName="Manager",Password="1234",Roles="Employee"},
            new UserCredentails{UserName="JhonSmith",Password="0000",Roles="Customer"}
        };
        public UserCredentails AuthenticateUser(UserCredentails creds)
        {
            UserCredentails user = null;
            foreach (var client in clientList)
            {
                if (client.UserName == creds.UserName && client.Password == creds.Password && client.Roles == creds.Roles)
                {
                    user = new UserCredentails { UserName = creds.UserName, Password = creds.Password, Roles = creds.Roles };
                }
            }
            return user;
        }
        public string GenerateJSONWebToken(UserCredentails userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, userInfo.Roles)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
