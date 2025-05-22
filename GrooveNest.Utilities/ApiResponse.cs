namespace GrooveNest.Utilities
{
    public class ApiResponse<T>(bool success, string message, T? data = default)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;


        public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>(false, message, default);
        }
    }
}
