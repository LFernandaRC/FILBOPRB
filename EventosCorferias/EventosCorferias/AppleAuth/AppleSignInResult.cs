namespace EventosCorferias.AppleAuth
{
    public interface IAppleSignInService
    {
        Task<AppleSignInResult?> SignInAsync(Action<string>? onLog = null);
    }
}
