using System.Net.Mime;
using System.Text.Json;

namespace BackendService.Helper.Responses
{
    public partial class ResponseMessage
    {
        public string? Header { get; set; }
        public string? Detail { get; set; }
        public string Note { get; set; } = "";
        public dynamic? Data { get; set; }
    }
    public static class ResponseMessageExtensions
    {
        public const string SuccessHeader = "Sukses!";
        public const string FailHeader = "Gagal!";

        public static class Database
        {
            public const string DataNotFound = "Data Tidak Dapat Ditemukan";
        }

        private static class ContentType
        {
            public const string ApplicationJson = "application/json";
        }

        public static async Task NotFoundResponse(this HttpResponse response, string message)
        {
            response.ContentType = ContentType.ApplicationJson;
            response.StatusCode = StatusCodes.Status404NotFound;
            var responseMessage = new ResponseMessage
            {
                Header = FailHeader,
                Detail = message
            };

            await response.WriteAsync(JsonSerializer.Serialize(responseMessage));
        }
    }
}
