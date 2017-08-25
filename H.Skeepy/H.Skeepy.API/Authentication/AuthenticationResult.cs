namespace H.Skeepy.API.Authentication
{
    public class AuthenticationResult
    {
        public static readonly AuthenticationResult Successful = new AuthenticationResult(true);
        public static readonly AuthenticationResult Failed = new AuthenticationResult(false);

        private AuthenticationResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public readonly bool IsSuccessful = false;
    }
}