using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class CallWebAPI
    {
        private IEnumerable<GitHubBranch>? branches = Array.Empty<GitHubBranch>();
        private bool getBranchesError;
        private bool shouldRender;
        protected override bool ShouldRender() => shouldRender;
        protected override async Task OnInitializedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            var client = ClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream);
            }
            else
            {
                getBranchesError = true;
            }

            shouldRender = true;
        }

        public class GitHubBranch
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }
    }
}