namespace GitArApi.Common.Constants;

public static class PasswordConstants
{
    public const int IterationCount = 100_000;
    public const int SaltSize = 16;
    public const int KeySize = 32;
}

public static class JWTConstants
{
    public const string Issuer = "VioleElnInc.";

    public const string SecretKey = "ThisIsAVerySecretKey";
}