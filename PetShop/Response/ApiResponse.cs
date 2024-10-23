using System.Net;

namespace PetShop.Response;

public class ApiResponse<TModel>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    private object? _data;
    public object Data => _data ?? new { };

    public ApiResponse(TModel data)
    {
        StatusCode = HttpStatusCode.OK;
        Success = true;
        _data = data;
    }

    public ApiResponse(string message, HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        Success = false;
    }

    public static ApiResponse<TModel> Ok(TModel data)
    {
        return new ApiResponse<TModel>(data);
    }

    public static ApiResponse<TModel> Error(string message, HttpStatusCode statusCode)
    {
        return new ApiResponse<TModel>(message, statusCode);
    }
}
