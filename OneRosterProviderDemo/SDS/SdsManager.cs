using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sds
{
    public class SdsManager
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string profileUrl = "beta/education/synchronizationProfiles";
        private string _token;

        public SdsManager(string token)
        {
            _token = token;
        }

        public async Task<HttpResponseMessage> DeleteProfileAsync(string profileId)
        {
            return await DeleteGraphAsync(profileUrl + $"/{profileId}");
        }

        public async Task<HttpResponseMessage> ManageProfileAsync(string profileId, string action)
        {
            return await PostGraphAsync(profileUrl + $"/{profileId}/{action}", string.Empty);
        }

        public async Task<HttpResponseMessage> QueryProfileAsync(string profileId)
        {
            return await QueryGraphAsync(profileUrl + $"/{profileId}");
        }

        public async Task<HttpResponseMessage> QueryProfileStatusAsync(string profileId)
        {
            return await QueryGraphAsync(profileUrl + $"/{profileId}/profileStatus");
        }

        public async Task<HttpResponseMessage> PostProfileAsync(string requestBody)
        {
            return await PostGraphAsync(profileUrl, requestBody);
        }

        public async Task<HttpResponseMessage> QueryAllProfilesAsync()
        {
            return await QueryGraphAsync(profileUrl);
        }

        public async Task<HttpResponseMessage> QuerySkusAsync()
        {
            return await QueryGraphAsync("/v1.0/subscribedSkus");
        }

        private async Task<HttpResponseMessage> PostGraphAsync(string relativeUrl, string requestBody)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, $"https://graph.microsoft.com/{relativeUrl}")
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await Client.SendAsync(req);
        }

        private async Task<HttpResponseMessage> DeleteGraphAsync(string relativeUrl)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"https://graph.microsoft.com/{relativeUrl}");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await Client.SendAsync(req);
        }

        private async Task<HttpResponseMessage> QueryGraphAsync(string relativeUrl)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"https://graph.microsoft.com/{relativeUrl}");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await Client.SendAsync(req);
        }
    }
}
