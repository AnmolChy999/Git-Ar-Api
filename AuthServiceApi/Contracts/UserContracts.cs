namespace GitArApi.AuthServiceApi.Contracts;

public class UserRegisterRequest
{
    public string? UserName { get; set; }

    public string? Password { get; set; }
}

public class UserLoginRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}