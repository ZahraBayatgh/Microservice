namespace HttpAggregator.Options
{
    public class WebApplicationOptions
    {
        public const string WebApplication = "Service2Api";
        public string BaseAddress { get; set; }
        public string GetValues() => $"/api/values";
    }
}
