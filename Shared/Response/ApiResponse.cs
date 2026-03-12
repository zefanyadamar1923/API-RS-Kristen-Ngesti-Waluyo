namespace Api_RS_Kristen_Ngesti_Waluyo.Shared.Models.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public ApiData<T>? Data { get; set; }

        public ApiResponse(bool success, T response, int code, string message)
        {
            Success = success;
            Data = new ApiData<T>
            {
                Metadata = new Metadata { Code = code, Message = message },
                Response = response
            };
        }
    }

    public class ApiData<T>
    {
        public Metadata? Metadata { get; set; }
        public T? Response { get; set; }
    }

    public class Metadata
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}
