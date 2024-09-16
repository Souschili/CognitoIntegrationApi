using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ServicesLayer.DTOs;
using ServicesLayer.Helpers;
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
        private readonly AmazonCognitoIdentityProviderClient _client;
        //// temp 
        private readonly string poolid = "us-east-1_x3HcsF0z4";


        public AdminCognitoUserService()
        {
            _client=CognitoClientFactory.GetClient();
        }

        public async Task AdminSetPasswordAsync(string userName, string password)
        {
            //var setPassword = new AdminSetUserPasswordRequest
            //{
            //    Username = userName,
            //    Password = password,
            //    UserPoolId = poolid,
            //    Permanent = true
            //};

            //await 
            throw new NotImplementedException();
        }

        public Task AdminSignUpUserAsync(SignUpRequestDto signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
