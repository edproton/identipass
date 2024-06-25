using Application.Services;

namespace Infra.Services;

using BCrypt.Net;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashPassword)
    {
        return BCrypt.Verify(password, hashPassword);
    }
}