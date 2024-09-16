using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs;
using ServicesLayer.Helpers;

namespace CognitoIntegrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AmazonCognitoIdentityProviderClient _client;

        public AuthController()
        {
            _client=CognitoClientFactory.GetClient();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpUser(SignUpRequestDto signUpRequest)
        {
            const string poolId = "us-east-1_x3HcsF0z4";
            //DTO fields
            var user = new
            {
                Name = "Demo2",
                Password="String_1982",
                Email="greatdragone735@gmail.com",
                Phone="+994509775415"
            };


            List<AttributeType> attributes = new()
            {
                 new AttributeType
                {
                     Name = "email",
                    Value=user.Email
                },
                new AttributeType
                {
                    Name="phone_number",
                    Value=user.Phone
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

            var signUprequest = new AdminCreateUserRequest
            {
                Username =user.Name,
                TemporaryPassword=user.Password,
                MessageAction=MessageActionType.SUPPRESS,
                DesiredDeliveryMediums = {"EMAIL"},
                ForceAliasCreation = false,
                UserPoolId=poolId,
                UserAttributes=attributes,
            };

            var signUpResponce=await _client.AdminCreateUserAsync(signUprequest);

            var passrequest = new AdminSetUserPasswordRequest
            {
                Password=user.Password,
                Username=user.Name,
                Permanent=true,
                UserPoolId = poolId,
            };

            await _client.AdminSetUserPasswordAsync(passrequest);   

            return Ok(signUpResponce);
        }
    }
}
