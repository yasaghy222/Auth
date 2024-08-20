using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Auth.Contracts.Common;
using Auth.Contracts.Request;

namespace Auth.Shared.RequestPipeline
{
    internal sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) :
     IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
        where TResponse : IErrorOr
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(ct);

            TResponse response = await next();

            await _unitOfWork.CommitAsync(transaction, ct);

            return response;
        }
    }
}