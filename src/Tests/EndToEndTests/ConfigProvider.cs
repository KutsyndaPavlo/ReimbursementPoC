namespace EndToEndTests
{
    public class ConfigProvider
    {
        public static string GetIdentityApiUrl()
        {
            return "https://localhost:8500";
        }

        public static string GetApiGatewayUrl()
        {
            return "https://localhost:8504";
        }
    }
}
