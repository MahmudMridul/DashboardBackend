using System.Net;

namespace DashboardBackend.Models
{
    public class APIResponse
    {
        public object? Data { get; set; } = null;
        public bool IsSuccess { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
