using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class ApiResponseWithData<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponseWithData<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new ApiResponseWithData<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }

        public static ApiResponseWithData<T> ErrorResponse(string message, IEnumerable<ValidationErrorDetail>? errors = null, T? data = default)
        {
            return new ApiResponseWithData<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? Enumerable.Empty<ValidationErrorDetail>(),
                Data = data
            };
        }
    }
}
