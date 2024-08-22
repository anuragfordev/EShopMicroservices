using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TResposne>: IRequest<TResposne>
        where TResposne : notnull
    {
    }
}
