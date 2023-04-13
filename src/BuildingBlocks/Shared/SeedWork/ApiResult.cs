using System.Text.Json.Serialization;

namespace Shared.SeedWork
{
  public class ApiResult<T>
  {
    public bool IsSuccessded { get; set; }

    public string Message { get; set; }

    public T Data { get; }

    [JsonConstructor]
    public ApiResult(bool isSucceeded, string message = null)
    {
      IsSuccessded = isSucceeded;
      Message = message;
    }

    public ApiResult(bool isSucceeded, T data, string message = null)
    {
      IsSuccessded = isSucceeded;
      Message = message;
      Data = data;
    }
  }
}
