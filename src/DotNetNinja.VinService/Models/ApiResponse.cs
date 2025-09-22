using System.Net;

namespace DotNetNinja.VinService.Models;

public class ApiResponse
{
    public int Status { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess => Status is >= 200 and < 300;

    public static ApiResponse Success(HttpStatusCode status) => new() { Status = (int)status };
    
    public static ApiResponse Error(HttpStatusCode status, string errorMessage) => new() { Status = (int)status, ErrorMessage = errorMessage };
}

public class ApiResponse<T>: ApiResponse where T: IModel
{
    public T? Data { get; set; }


    public static ApiResponse<T> Success(HttpStatusCode status, T data) => new() { Status = (int)status, Data = data};
}