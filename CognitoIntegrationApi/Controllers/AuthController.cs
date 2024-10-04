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
