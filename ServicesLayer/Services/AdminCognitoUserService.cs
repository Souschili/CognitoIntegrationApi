using ServicesLayer.DTOs;
using ServicesLayer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services
{
    internal class AdminCognitoUserService : IAdminCognitoUserService
    {
        public Task AdminSetPasswordAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task AdminSignUpUserAsync(SignUpRequestDto signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
