namespace Service2.IntegrationTest.Config
{
    public static class AppDomainVariable
    {
        public static string AppSettingName => $"appsettings.json";

        public const string BaseAddress = "http://localhost:9003";
        public static string EnvironmentName = "Development";
    }
}
