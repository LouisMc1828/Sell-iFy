using Newtonsoft.Json;

namespace Sellify.Api.Errors;

public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string[]? Message { get; set; }


    public CodeErrorResponse(int statusCode, string[]? message = null)
    {
        StatusCode = statusCode;
        if (message is null)
        {
            Message = new string[0];
            var text = GetDefaultMessageStatusCode(statusCode);
            Message[0] = text;
        }
        else
        {
            Message = message;
        }
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Send Request with errors",
            401 => "Not Authorization",
            404 => "Not Found",
            500 => "Internal Server Error",
            _=> string.Empty
        };
    }
}

