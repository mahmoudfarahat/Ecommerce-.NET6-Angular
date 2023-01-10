namespace ecommerce.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a Bad request",
                401 => "You are not Authorized",
                404 => "Resource not found",
                500 => "Server is died!!",
                _ =>String.Empty
            };
        }
    }
}
