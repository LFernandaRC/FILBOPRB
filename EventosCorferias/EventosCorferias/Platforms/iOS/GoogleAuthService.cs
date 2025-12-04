

namespace EventosCorferias.GoogleAuth
{
    public partial class GoogleAuthService
    {
        private TaskCompletionSource<GoogleUserDTO> tcs;

        public GoogleAuthService()
        {
        }

        public Task<GoogleUserDTO> AuthenticateAsync()
        {
            return null;
        }

        private void PreparePresentedViewController()
        {

        }

        public async Task<GoogleUserDTO> GetCurrentUserAsync()
        {
            return null;
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}