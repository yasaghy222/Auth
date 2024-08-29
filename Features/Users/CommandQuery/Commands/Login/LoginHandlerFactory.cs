using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.Contracts.Enums;
using MediatR;
using ErrorOr;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public interface ILoginHandler
    {
        Task<ErrorOr<TokenResponse>> HandleLogin(LoginCommand command, CancellationToken ct);
    }

    public class LoginHandlerFactory(IServiceProvider serviceProvider)
        : IRequestHandler<LoginCommand,
         ErrorOr<TokenResponse>>
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<ErrorOr<TokenResponse>> Handle(
            LoginCommand command, CancellationToken ct)
        {
            //TODO: validate organization

            ILoginHandler loginHandler = command.Type switch
            {
                UserLoginType.Password => _serviceProvider.GetRequiredService<LoginByPasswordHandler>(),
                _ => throw new ArgumentNullException(nameof(command)),
            };

            return await loginHandler.HandleLogin(command, ct);
        }
    }
}