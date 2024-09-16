using ServicesLayer.DTOs;


namespace ServicesLayer.Services.Contracts
{
    public interface IAdminCognitoUserService
    {
        Task AdminSignUpUserAsync(SignUpRequestDto signUpRequest);
        Task AdminSetPasswordAsync(string userName, string password);

    }
}
