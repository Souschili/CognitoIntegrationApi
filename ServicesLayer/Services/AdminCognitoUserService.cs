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
            _client = CognitoClientFactory.GetClient();
        }

        public async Task AdminSetPasswordAsync(string userName, string password)
        {
            var setPasswordRequest = new AdminSetUserPasswordRequest
            {
                Username = userName,
                Password = password,
                UserPoolId = poolid,
                Permanent = true
            };

            var setPasswordResponce = await _client.AdminSetUserPasswordAsync(setPasswordRequest);

        }

        public Task AdminSignUpUserAsync(SignUpRequestDto signUpRequest)
        {
            List<AttributeType> attributes = new()
            {
                 new AttributeType
                {
                     Name = "email",
                    Value=signUpRequest.Email
                },
                new AttributeType
                {
                    Name="phone_number",
                    Value=signUpRequest.Phone
                },
                 new AttributeType{
                      Name= "email_verified",
                      Value= "true"
                   },
                   new AttributeType
                   {
                       Name="phone_number_verified",
                       Value="true"
                   }
            };
            throw new NotImplementedException();
        }
    }
}
