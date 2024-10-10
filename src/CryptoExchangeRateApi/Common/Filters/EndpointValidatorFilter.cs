using System.Net;
using FluentValidation.Results;

namespace CryptoExchangeRateApi.Common.Filters;

internal sealed class EndpointValidatorFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    private IValidator<T> Validator => validator;
  
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        T? inputData = context.GetArgument<T>(0);

        if (inputData is not null)
        {
            ValidationResult validationResult = await Validator.ValidateAsync(inputData);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
            }
        }

        return await next.Invoke(context);
    }
}