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
    }
}
