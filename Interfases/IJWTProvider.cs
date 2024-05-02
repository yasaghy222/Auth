using Authenticate.Entities;

namespace Authenticate.Interfaces
{
    public interface IJWTProvider
    {
        string Generate(User user);
    }
}