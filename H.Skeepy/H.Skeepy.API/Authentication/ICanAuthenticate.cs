using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public interface ICanAuthenticate<T>
    {
        Task<AuthenticationResult> Authenticate(T identifier);
    }
}