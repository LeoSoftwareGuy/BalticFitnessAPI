using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery,TResponse>
        where TQuery : IQuery<TResponse> // as it might inherit from ICommand
        where TResponse : notnull
    {
    }
}
