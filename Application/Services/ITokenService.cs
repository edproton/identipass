namespace Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Guid GetUserIdFromToken(string token);
}