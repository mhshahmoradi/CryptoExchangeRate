using CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates.Models;

namespace CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates.Validator;

public sealed class GetCryptoRateValidator : AbstractValidator<CryptoRateRequest>
{
    public GetCryptoRateValidator()
    {
        RuleFor(x => x.Symbol)
            .NotNull()
            .NotEmpty()
            .Length(3,5)
            .Matches("^[a-zA-Z]+$")
            .WithMessage("Symbol must only contain English letters, and be between 3 to 5 characters long.");
    }
}