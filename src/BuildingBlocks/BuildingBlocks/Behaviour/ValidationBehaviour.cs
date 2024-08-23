using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validatorsResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var errors = validatorsResults
                .Where(x => x.Errors.Any())
                .SelectMany(v => v.Errors)
                .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
