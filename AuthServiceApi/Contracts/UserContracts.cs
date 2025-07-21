namespace GitArApi.AuthServiceApi.Contracts;

public class UserRegisterRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class UserLoginRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}