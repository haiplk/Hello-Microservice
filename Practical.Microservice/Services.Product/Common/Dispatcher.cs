using Services.Product.Common.Commands;
using Services.Product.Common.Queries;

namespace Services.Product.Common
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = serviceProvider.GetService(handlerType);
            await handler.HandleAsync((dynamic)command, cancellationToken);
        }

        public async Task<TResult> DispatchAsync<TResult>(ICommand command, CancellationToken cancellationToken = default)
        {
            Type type = typeof(ICommandHandler<,>);
            Type[] typeArgs = { command.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = serviceProvider.GetService(handlerType);
            return await handler.HandleAsync((dynamic)command, cancellationToken);
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery command, CancellationToken cancellationToken = default)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { command.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = serviceProvider.GetService(handlerType);
            var result = await handler.HandleAsync((dynamic)command, cancellationToken);
            return result;
        }
    }
}
