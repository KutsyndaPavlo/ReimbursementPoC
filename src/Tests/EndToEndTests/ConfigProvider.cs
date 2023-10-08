namespace EndToEndTests
{
    public class ConfigProvider
    {
        public static string GetIdentityApiUrl()
        {
            return "https://ri-identity-api627w7ovra6cy4.azurewebsites.net";// "https://localhost:8500";
        }

        public static string GetApiGatewayUrl()
        {
            return "http://52.224.186.68";// "https://localhost:8504";
        }
    }
}
