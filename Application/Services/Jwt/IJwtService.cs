using Domain.Models.User;
namespace Application.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
        int TokenExpiryMinutes { get; }
    }

}
