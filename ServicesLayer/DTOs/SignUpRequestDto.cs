
namespace ServicesLayer.DTOs
{
    public record class SignUpRequestDto(
        string Name,
        string Email,
        string Password,
        string Phone
        );

}
