namespace Application.Errors;

public static class UserErrors
{
    public static Error EmailAlreadyExists(string email) => Error.Conflict("email_already_exists", $"Email {email} already exists");

    public static Error InvalidCredentials => Error.Validation("invalid_credentials", "Invalid credentials");
    
    public static Error PasswordsDoNotMatch => Error.Validation("passwords_do_not_match", "Passwords do not match");

    public static Error UsernameOrEmailRequired => Error.Validation("username_or_email_required", "Username or email is required");

    public static Error UserNotFound(string user) => Error.NotFound("user_not_found", $"User {user} not found");
}

public static class RoleErrors
{
    public static Error RoleAlreadyExists(string role) => Error.Conflict("role_already_exists", $"Role {role} already exists");
    
    public static Error RoleNotFound(string role) => Error.NotFound("role_not_found", $"Role {role} not found");

    public static Error UserAlreadyInRole(string? user, string role) => Error.Conflict("user_already_in_role", $"User {user} is already in role {role}");

    public static Error UserNotInRole(string user, string role) => Error.NotFound("user_not_in_role", $"User {user} is not in role {role}");
}

public static class RequestErrors
{
    public static Error InvalidRequest => Error.Validation("invalid_request", "Invalid request");
}

public static class ClaimErrors
{
    public static Error UserAlreadyHasClaim(string user, string type, string value) => Error.Conflict("user_already_has_claim", $"User {user} already has claim {type}:{value}");
    public static Error ClaimAlreadyExists => Error.Conflict("claim_already_exists", $"Claim already exists");
    
    public static Error ClaimNotFound(string type, string value) => Error.NotFound("claim_not_found", $"Claim {type}:{value} not found");
}

public static class RefreshTokensErrors
{
    public static Error InvalidRefreshToken(string refreshToken) => Error.NotFound("invalid_refresh_token", $"Invalid refresh token {refreshToken}");
    
    public static Error RefreshTokenNotFound(string refreshToken) => Error.NotFound("refresh_token_not_found", $"Refresh token {refreshToken} not found");
}