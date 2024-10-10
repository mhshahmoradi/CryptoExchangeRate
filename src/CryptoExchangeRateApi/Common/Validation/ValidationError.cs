namespace CryptoExchangeRateApi.Common.Validation;

public readonly struct ValidationError(int code, string message)
{
    public int Code { get; init; } = code;
    public string Message { get; init; } = message;
    public static ValidationError Failure(string message) => new ValidationError(400, message);
    public static ValidationError Failure(int code, string message) => new ValidationError(code, message);
}