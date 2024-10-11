using System;
using FluentValidation;
using MediatR;

namespace MQBulkInsert.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Where(f => f != null)
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
        catch (System.Exception ex)
        {
            var err = ex;
            throw;
        }
    }
}
