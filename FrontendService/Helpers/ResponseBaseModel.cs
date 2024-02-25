namespace FrontendService.Helpers
{
    public class ResponseBaseModel
    {
        public bool IsError { get; set; } = false;
        public string? Message { get; set; }
    }
}
