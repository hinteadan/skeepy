namespace H.Skeepy.API.Authentication
{
    public interface ICanAuthenticate<T>
    {
        AuthenticationResult Authenticate(T identifier);
    }
}