using Services.Product.Common.Commands;
using Services.Product.Common.Queries;

namespace Services.Product.Common
{
    public interface IDispatcher
    {
        Task DispatchAsync(ICommand command, CancellationToken cancellationToken = default);

        Task<TResult> DispatchAsync<TResult>(ICommand command, CancellationToken cancellationToken = default);

        Task<TResult> DispatchAsync<TResult>(IQuery command, CancellationToken cancellationToken = default);
    }
}
