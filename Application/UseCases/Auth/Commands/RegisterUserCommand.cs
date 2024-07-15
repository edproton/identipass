namespace Application.UseCases.Auth.Commands;

public record RegisterUserCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string? Username) : IRequest<Result<Guid>>;

public class RegisterUserCommandHandler(
    ILogger logger,
    IUsersRepository usersRepository,
    IPasswordService passwordService) : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return Result.Failure<Guid>(UserErrors.PasswordsDoNotMatch);
        }
        
        var existingUserByEmail = await usersRepository.GetUserByEmail(request.Email, cancellationToken);
        if (existingUserByEmail is not null)
        {
            return Result.Failure<Guid>(UserErrors.EmailAlreadyTaken);
        }

        if (request.Username is not null)
        {
            var existingUserByName = await usersRepository.GetUserByUsername(request.Username, cancellationToken);
            if (existingUserByName is not null)
            {
                return Result.Failure<Guid>(UserErrors.UsernameAlreadyTaken);
            }
        }
  
        var user = new User
        {
            Email = request.Email,
            Password = passwordService.HashPassword(request.Password),
            Username = request.Username,
        };

        await usersRepository.AddAsync(user, cancellationToken);

        logger.LogInformation($"[User] {user.Id} registered");

        return Result.Success(user.Id);
    }
}