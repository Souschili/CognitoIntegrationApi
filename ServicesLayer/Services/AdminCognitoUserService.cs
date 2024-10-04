using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ServicesLayer.DTOs;
using ServicesLayer.Services.Contracts;

namespace ServicesLayer.Services
{
    public class AdminCognitoUserService : IAdminCognitoUserService
    {
        private readonly AmazonCognitoIdentityProviderClient _client;
        // temp 
        private readonly string poolid = "us-east-1_x3HcsF0z4";


        public AdminCognitoUserService(AmazonCognitoIdentityProviderClient client)
        {
            _client = client;
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

        public Task AdminSignInUserAsync(SignInRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task AdminSignUpUserAsync(SignUpRequestDto signUpRequest)
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

            var adminCreaterequest = new AdminCreateUserRequest
            {
                Username = signUpRequest.Name,
                TemporaryPassword = signUpRequest.Password,
                MessageAction = MessageActionType.SUPPRESS,
                DesiredDeliveryMediums = { "EMAIL" },
                ForceAliasCreation = false,
                UserPoolId = poolid,
                UserAttributes = attributes,
            };

            await _client.AdminCreateUserAsync(adminCreaterequest);


            // set permanent password
            await AdminSetPasswordAsync(signUpRequest.Name, signUpRequest.Password);


        }
    }
}
