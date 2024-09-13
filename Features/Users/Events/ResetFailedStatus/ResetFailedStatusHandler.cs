using MediatR;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Requests;
using Auth.Contracts.Common;

namespace Auth.Features.Users.Events.ResetFailedStatus
{
    public class ResetFailedStatusHandler(
        IUnitOfWork unitOfWork,
        IOrganizationRepository userRepository,
        ILogger<ResetFailedStatusHandler> logger)
        : INotificationHandler<ResetFailedStatusEvent>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IOrganizationRepository _userRepository = userRepository;
        private readonly ILogger<ResetFailedStatusHandler> _logger = logger;

        public async Task Handle(ResetFailedStatusEvent @event, CancellationToken ct)
        {
            ResetFailedStatusRequest request = new()
            {
                Id = @event.Id,
            };

            await _userRepository.ResetFailedLoginInfoAsync(request, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogWarning("User: {@event.Username}, block status has been reset",
                 @event.Username);
        }
    }
}
