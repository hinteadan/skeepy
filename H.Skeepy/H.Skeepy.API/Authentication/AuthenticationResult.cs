namespace H.Skeepy.API.Authentication
{
    public class AuthenticationResult
    {
        public static AuthenticationResult Successful(string token) { return new AuthenticationResult(true, token); }
        public static readonly AuthenticationResult Failed = new AuthenticationResult(false, null);

        private AuthenticationResult(bool isSuccessful, string token)
        {
            IsSuccessful = isSuccessful;
            Token = token;
        }

        public readonly bool IsSuccessful = false;
        public readonly string Token;
    }
}