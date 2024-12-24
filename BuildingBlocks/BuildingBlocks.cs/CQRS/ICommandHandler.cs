using MediatR;

namespace BuildingBlocks.CQRS
{

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
              where TCommand : ICommand<Unit>;  //as it might inherit from IQuery

    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
