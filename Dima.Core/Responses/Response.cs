using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>
{
    private readonly int _code;

    // parameterless
    [JsonConstructor]
    public Response() => _code = Configuration.DEFAULT_STATUS_CODE;

    public Response(
        TData? data, 
        int code = Configuration.DEFAULT_STATUS_CODE, 
        string? message = "")
    {
        Data = data;
        Message = message;
        _code = code;
    }
    
    public TData Data { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore]
    public bool isSuccess => _code is >= 200 and <= 299;
}