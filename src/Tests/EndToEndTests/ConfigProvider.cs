namespace EndToEndTests
{
    public class ConfigProvider
    {
        public static string GetIdentityApiUrl()
        {
            return "https://riidndev.azurewebsites.net";// "https://localhost:8500";
        }

        public static string GetApiGatewayUrl()
        {
            return "http://4.153.147.22"; //"https://localhost:8504"; 
        }
    }
}
