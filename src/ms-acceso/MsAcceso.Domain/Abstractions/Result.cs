using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace MsAcceso.Domain.Abstractions;


public class Result
{
    protected internal Result(bool isSuccess, Error error, string message)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }


        if(!isSuccess  && error == Error.None )
        {
            throw new InvalidOperationException();
        }

        Message = isSuccess ? message : error.Name;
        Status = isSuccess ? (int)HttpStatusCode.OK : error.Code;
        IsFailure = !isSuccess;
        Messages = [];
    }

    public int Status {get;}
    public string Message {get;}
    public List<string> Messages {get;set;} 
    public bool IsFailure {get;}
    public static Result Success() => new(true, Error.None,"");

    public static Result Failure(Error error) => new(false, error,"");

    public static Result<TPayload?> Success<TPayload>(TPayload payload, string message) 
        => new(payload, true, Error.None, message);

    public static Result<TPayload?> Failure<TPayload>(Error error) 
        => new(default, false, error,"");

    public static Result<TPayload?> Create<TPayload>(TPayload? payload) 
        => Success(payload, "") ;

}

public class Result<TPayload> : Result
{

    public Result(TPayload? payload, bool isSuccess, Error error, string message) 
    : base(isSuccess, error,message)
    {
        Payload = payload;
    }

    public TPayload? Payload {get;}


    public static implicit operator Result<TPayload?>(TPayload payload) => Create(payload);


}