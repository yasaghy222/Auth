using Authenticate.Context;
using Authenticate.Interfaces;
using FluentValidation;

namespace Authenticate.Models
{
    public class ToolsService(IJWTProvider provider, AuthenticateContextDb context)
    {
        public IJWTProvider Jwt { get; private set; } = provider;
        public AuthenticateContextDb Db { get; private set; } = context;
    }
}