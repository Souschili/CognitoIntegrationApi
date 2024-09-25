using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs;
using ServicesLayer.Services.Contracts;

namespace CognitoIntegrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdminCognitoUserService _adminCognitoService;

        public AuthController(IAdminCognitoUserService adminCognitoService)
        {
            _adminCognitoService = adminCognitoService;
        }

        //TODO: remove
        [HttpPost("Demo")]
        public async Task<IActionResult> SignUpUserDemo(SignUpRequestDto signUpRequest)
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

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequestDto requestDto)
        {
            try
            {
                await _adminCognitoService.AdminSignUpUserAsync(requestDto);
                return Ok(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
