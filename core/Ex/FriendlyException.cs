using System.Net;

namespace core.Ex;

public class FriendlyException : Exception
{
    public FriendlyException(string message, HttpStatusCode code)
    {
        this.Message = message;
        this.Code = code;
    }

    public override string Message { get; }

    public HttpStatusCode Code { get; set; }
}