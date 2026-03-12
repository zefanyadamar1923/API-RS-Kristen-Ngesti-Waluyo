namespace Api_RS_Kristen_Ngesti_Waluyo.Shared.Models.Response
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; }
        public ApiErrorData? Data { get; set; }

        public ApiErrorResponse(int code, string message)
        {
            Success = false;
            Data = new ApiErrorData
            {
                Metadata = new Metadata { Code = code, Message = message }
            };
        }
    }

    public class ApiErrorData
    {
        public Metadata? Metadata { get; set; }
    }
}
