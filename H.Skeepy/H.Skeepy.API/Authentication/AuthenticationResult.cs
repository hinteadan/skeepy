namespace H.Skeepy.API.Authentication
{
    public class AuthenticationResult
    {
        public static AuthenticationResult Successful(Token token) { return new AuthenticationResult(true, token); }
        public static readonly AuthenticationResult Failed = new AuthenticationResult(false, null);

        private AuthenticationResult(bool isSuccessful, Token token)
        {
            IsSuccessful = isSuccessful;
            Token = token;
        }

        public readonly bool IsSuccessful = false;
        public readonly Token Token;
    }
}