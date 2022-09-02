namespace WebApi.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode,string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(StatusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "El Request enviado contiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontró el item buscado",
                500 => "Se producieron errores en el servidor",
                _ => null
            };
        }
    }
}
