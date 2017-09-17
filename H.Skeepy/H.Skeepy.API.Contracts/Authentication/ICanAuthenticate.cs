using System.Threading.Tasks;

namespace H.Skeepy.API.Contracts.Authentication
{
    public interface ICanAuthenticate<T>
    {
        Task<AuthenticationResult> Authenticate(T identifier);
    }
}