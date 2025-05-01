namespace Core.Domain.ValueObjects
{
    public class Result<T>(T value, bool isSuccess, string? errorMessage, int? statusCode)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? ErrorMessage { get; } = errorMessage;
        public T Value { get; } = value;
        public int? StatusCode { get; } = statusCode;

        public static Result<T> Success(T value) => new Result<T>(value, true, null, null);

        public static Result<T> Failure(string errorMessage) => new Result<T>(default, false, errorMessage, null);
    }
}
