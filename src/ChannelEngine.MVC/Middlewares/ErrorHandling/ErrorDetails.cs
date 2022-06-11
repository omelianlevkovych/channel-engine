using Newtonsoft.Json;

namespace ChannelEngine.MVC.Middlewares.ErrorHandling
{
    public record ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
