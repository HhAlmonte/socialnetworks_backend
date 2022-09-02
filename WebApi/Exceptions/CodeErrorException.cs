namespace WebApi.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public CodeErrorException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        public string Details { get; set; }
    }
}
