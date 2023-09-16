

using DiakuSoft.Server.Domain.Dtos.Response;
using System.Net;

namespace DiakuSoft.Server.Domain.Dtos;

public class ApiResponse<T>
{
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
    public bool Succeed { get; set; } = true;
    public string? Message { get; set; }
    public T? Data { get; set; }
    public Pagination? Pagination { get; set; }

    /// <summary>
    /// Incase of succeesfull request
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pagination"></param>
    public ApiResponse(T data,Pagination? pagination=null)
    {
        Data = data;
        Pagination = pagination;
    }
    /// <summary>
    /// Incase of unsuccessfull request
    /// </summary>
    /// <param name="httpStatusCode"></param>
    /// <param name="errorMessage"></param>
    public ApiResponse(HttpStatusCode httpStatusCode,string errorMessage)
    {
        HttpStatusCode = httpStatusCode;
        Message = errorMessage;
        Succeed = false;
    }
}
