namespace Application.Common.Models;

public class Result
{
    internal Result(
        bool succeeded, 
        object? data, 
        IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }
    public object? Data { get; init; }
    public string[] Errors { get; init; }

    public static Result Success(object? data = null)
        => new Result(true, data, Array.Empty<string>());

    public static Result Failure(IEnumerable<string> errors)
        => new Result(false, null, errors);

    public static Result Failure(params string[] errors)
        => new (false, null, errors);
}