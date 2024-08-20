using MediatR;

namespace Auth.Contracts.Request
{
    public interface ICommand : IRequest;
    public interface ICommand<TResponse> : IRequest<TResponse>;
}