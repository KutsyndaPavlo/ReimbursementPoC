namespace ReimbursementPoC.Administration.Infrastructure
{
    internal class HttpService
    {
        private readonly HttpClient _client;

        public HttpService(HttpClient client)
        {
            _client = client;
        }

        //public async Task<GitHubUser?> GetUserAsync(string username)
        //{
        //    GitHubUser? user = await client
        //        .GetFromJsonAsync<GitHubUser>($"users/{username}");

        //    return user;
        //}
    }
}
