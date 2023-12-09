using Microsoft.AspNetCore.Identity;
using MandD;

namespace Projec;

public interface IPasswordHasherWrapper
{
    string HashPassword(User user, string password);
    PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
}

public class PasswordHasherWrapper : IPasswordHasherWrapper
{
    private readonly PasswordHasher<User> _passwordHasher;

    public PasswordHasherWrapper(PasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    }
}