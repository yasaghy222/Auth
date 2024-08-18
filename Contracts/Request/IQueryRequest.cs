using MediatR;

namespace Auth.Contracts.Request
{
    public interface IQuery<Response> : IRequest<Response>;
}