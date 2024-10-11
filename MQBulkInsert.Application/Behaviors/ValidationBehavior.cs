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
        var context = new ValidationContext<TRequest>(request);
        var validationResults = _validators
            .Select(v => v.Validate(context))
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToList();

        if (validationResults.Any())
        {
            throw new ValidationException(validationResults);
        }

        return await next();
    }
}
