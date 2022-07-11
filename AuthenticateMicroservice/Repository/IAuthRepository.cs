using AuthenticateMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Repository
{
    public interface IAuthRepository
    {
        public UserCredentails AuthenticateUser(UserCredentails creds);
        public string GenerateJSONWebToken(UserCredentails userInfo);
    }
}
