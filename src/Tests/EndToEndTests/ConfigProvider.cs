namespace EndToEndTests
{
    public class ConfigProvider
    {
        public static string GetIdentityApiUrl()
        {
            return "https://localhost:8500"; //"https://ri-identity-api627w7ovra6cy4.azurewebsites.net";// 
        }

        public static string GetApiGatewayUrl()
        {
            return "https://localhost:8504"; //"http://52.224.186.68";// 
        }
    }
}
