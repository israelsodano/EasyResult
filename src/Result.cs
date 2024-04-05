namespace EResult;

public record struct Result(Error Error = null) 
{
    public readonly bool Success => Error == null;

    public static implicit operator Result(string code) =>
        code is not null ? new(code) : new();

    public static implicit operator Result((string code, string description) cd) => 
        new(cd);

    public static implicit operator Error(Result r) =>
        r.Error;  
}

public record class Error(string Code, 
                          string Description = null)
{
    public static implicit operator Error(string code) =>
        code is not null ? new(code) : throw new ArgumentNullException(nameof(code));

    public static implicit operator Error((string code, string description)cd) =>
        cd.code is not null ? new(cd.code, cd.description) : throw new ArgumentNullException(nameof(cd.code));
}

public class ResultException : Exception 
{
    public ResultException(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public readonly string Code;
    public readonly string Description;
}

public record struct Result<T>(T Value, Error Error = null)
{
    public readonly bool Success => Error == null;

    public static implicit operator Result<T>(T val) =>
        new(val);

    public static implicit operator T (Result<T> r) =>
        r.Success ? r.Value : throw new ResultException(r.Error.Code, 
                                                        r.Error.Description) ;

    public static implicit operator Result<T>(string code) =>
        new(default, code);

    public static implicit operator Result<T>((string code, string description) cd) => 
        new(default, cd);

    public static implicit operator Result(Result<T> r) =>
        new(r.Error);

    public static implicit operator Error(Result<T> r) =>
        r.Error; 
}