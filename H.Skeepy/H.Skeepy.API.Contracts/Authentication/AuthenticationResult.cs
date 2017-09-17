namespace H.Skeepy.API.Contracts.Authentication
{
    public class AuthenticationResult
    {
        public static AuthenticationResult Successful(Token token) { return new AuthenticationResult(true, token); }
        public static AuthenticationResult Failed(AuthenticationFailureReason reason, Token failedToken = null) { return new AuthenticationResult(false, failedToken, reason); }

        private AuthenticationResult(bool isSuccessful, Token token, AuthenticationFailureReason failureReason)
        {
            IsSuccessful = isSuccessful;
            Token = token;
            FailureReason = failureReason;
        }

        private AuthenticationResult(bool isSuccessful, Token token)
            : this(isSuccessful, token, AuthenticationFailureReason.None)
        { }

        public readonly bool IsSuccessful = false;
        public readonly Token Token;
        public readonly AuthenticationFailureReason FailureReason = AuthenticationFailureReason.None;
    }
}