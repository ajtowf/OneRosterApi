using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using OneRosterProviderDemo.ViewModels;
using Sds;

namespace OneRosterProviderDemo.Controllers
{
    [Authorize]
    [Route("sds")]
    public class SdsController : Controller
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly ILogger<SdsController> _logger;
        private readonly IConfiguration _config;
        private SdsManager manager;

        public SdsController(IConfiguration config, ILogger<SdsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new CreateRestProfileModel
            {
                ConnectionUrl = $"{(Request.IsHttps ? "https" : "http")}://{Request.Host}/ims/oneroster/v1p1",
                DisplayName = "OneRoster API",
                ClientId = "contoso",
                ClientSecret = "contoso-secret",
                AzureDomain = _config.GetValue<string>("AzureDomain")
            };

            return View(model);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfileAsync([FromForm] CreateRestProfileModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            var profile = await GetProfileAsync(model.DisplayName);
            if (profile != null)
                return RedirectToAction("ProfilesAsync", new { profileId = profile["id"] });

            var profileToCreate = await GenerateProfileAsync(model);
            var createProfileRequest = await manager.PostProfileAsync(profileToCreate);
            var createProfileContent = await createProfileRequest.Content.ReadAsStringAsync();
            var createdProfile = JObject.Parse(createProfileContent);
            if (createdProfile.ContainsKey("error"))
            {
                var formattedError = createdProfile.ToString(Formatting.Indented);
                ViewBag.creationError = formattedError;

                _logger.LogError("Error while creating profile:\r\n" + formattedError);
                return View("Index", model);
            }

            _logger.LogInformation("Created profile:\r\n" + createdProfile.ToString(Formatting.Indented));

            return RedirectToAction("ProfilesAsync", new { profileId = createdProfile["id"] });
        }
        
        [Route("profiles")]
        public async Task<IActionResult> ProfilesAsync(string profileId)
        {
            if (manager == null)
            {
                manager = new SdsManager(await GetAccessTokenAsync());
            }

            var manageError = TempData["manageProfileError"];
            if (manageError != null)
            {
                ViewBag.manageProfileError = manageError;
            }

            ViewBag.profileId = profileId;

            var profileRequest = string.IsNullOrWhiteSpace(profileId) ? 
                await manager.QueryAllProfilesAsync() :
                await manager.QueryProfileAsync(profileId);
            
            var profile = await profileRequest.Content.ReadAsStringAsync();
            var profileDes = JsonConvert.DeserializeObject(profile);
            var profileSer = JsonConvert.SerializeObject(profileDes, Formatting.Indented);
            ViewBag.profile = profileSer;

            if (!string.IsNullOrWhiteSpace(profileId))
            {
                var profileStatusRequest = await manager.QueryProfileStatusAsync(profileId);
                var profileStatus = await profileStatusRequest.Content.ReadAsStringAsync();
                var profileStatusDes = JsonConvert.DeserializeObject(profileStatus);
                var profileStatusSer = JsonConvert.SerializeObject(profileStatusDes, Formatting.Indented);
                ViewBag.profileStatus = profileStatusSer;
            }
            else
            {
                ViewBag.profileStatus = string.Empty;
            }

            return View("Profiles");
        }

        [HttpPost]
        public async Task<IActionResult> ManageProfileAsync(string profileId, string action)
        {
            if (manager == null)
            {
                manager = new SdsManager(await GetAccessTokenAsync());
            }

            _logger.LogInformation($"Attempting {action} profile {profileId}");

            var manageRequest = action != "delete" ? 
                await manager.ManageProfileAsync(profileId, action) :
                await manager.DeleteProfileAsync(profileId);
            
            var resultContent = await manageRequest.Content.ReadAsStringAsync();
            var result = JObject.Parse(resultContent);
            var formattedResult = result.ToString(Formatting.Indented);
            if (result.ContainsKey("error"))
            {
                TempData["manageProfileError"] = formattedResult;
                _logger.LogError("Error while managing profile:\r\n" + formattedResult);

                return RedirectToAction("ProfilesAsync", new { profileId });
            }

            _logger.LogInformation($"Manage result (URL: {manageRequest.RequestMessage.RequestUri.AbsoluteUri}, StatusCode: {manageRequest.StatusCode})\r\n: {formattedResult}");
            return RedirectToAction("ProfilesAsync", new {profileId});
        }

        private async Task<JObject> GetProfileAsync(string profileName)
        {
            if (manager == null)
            {
                manager = new SdsManager(await GetAccessTokenAsync());
            }

            // find existing matching profile
            var res = await manager.QueryAllProfilesAsync();
            var profiles = (JArray)JObject.Parse(await res.Content.ReadAsStringAsync())["value"];

            foreach (var profile in profiles)
            {
                if ((string)profile["displayName"] == profileName)
                {
                    return (JObject)profile;
                }
            }

            return null;
        }
        
        private async Task<string> GetAccessTokenAsync()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

        // https://github.com/OfficeDev/O365-EDU-Tools/blob/master/SDSProfileManagementDocs/api/synchronizationProfile_create.md
        private async Task<string> GenerateProfileAsync(CreateRestProfileModel profile)
        {
            var skus = await GetOfficeSkusAsync();

            StringBuilder sb = new StringBuilder();
            using (JsonWriter writer = new JsonTextWriter(new StringWriter(sb)))
            {
                writer.WriteStartObject();

                writer.WritePropertyName("displayName");
                writer.WriteValue(profile.DisplayName);

                writer.WritePropertyName("dataProvider");
                writer.WriteStartObject();

                writer.WritePropertyName("@odata.type");
                writer.WriteValue("#microsoft.graph.educationoneRosterApiDataProvider");

                writer.WritePropertyName("connectionUrl");
                writer.WriteValue(profile.ConnectionUrl);

                writer.WritePropertyName("schoolsIds");
                writer.WriteStartArray();
                foreach (var schoolId in profile.SchoolIds.Split(','))
                {
                    writer.WriteValue(schoolId);
                }
                writer.WriteEndArray();

                writer.WritePropertyName("connectionSettings");
                writer.WriteStartObject();

                writer.WritePropertyName("@odata.type");
                writer.WriteValue("#microsoft.graph.educationSynchronizationOAuth1ConnectionSettings");

                writer.WritePropertyName("clientId");
                writer.WriteValue(profile.ClientId);

                writer.WritePropertyName("clientSecret");
                writer.WriteValue(profile.ClientSecret);

                writer.WriteEndObject();

                writer.WriteEndObject();

                writer.WritePropertyName("identitySynchronizationConfiguration");
                writer.WriteStartObject();

                writer.WritePropertyName("@odata.type");
                writer.WriteValue("#microsoft.graph.educationidentitycreationconfiguration");

                writer.WritePropertyName("userDomains");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("appliesTo");
                writer.WriteValue("student");

                writer.WritePropertyName("name");
                writer.WriteValue(profile.AzureDomain);
                writer.WriteEndObject();

                writer.WriteStartObject();
                writer.WritePropertyName("appliesTo");
                writer.WriteValue("teacher");

                writer.WritePropertyName("name");
                writer.WriteValue(profile.AzureDomain);
                writer.WriteEndObject();
                writer.WriteEndArray();

                writer.WriteEndObject();

                writer.WritePropertyName("licensesToAssign");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("appliesTo");
                writer.WriteValue("teacher");

                writer.WritePropertyName("skuIds");
                writer.WriteStartArray();
                if(skus.Item1 != null)
                    writer.WriteValue(skus.Item1);
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.WriteStartObject();
                writer.WritePropertyName("appliesTo");
                writer.WriteValue("student");

                writer.WritePropertyName("skuIds");
                writer.WriteStartArray();
                if(skus.Item2 != null)
                    writer.WriteValue(skus.Item2);
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.WriteEndArray();

                writer.WriteEndObject();

                return sb.ToString();
            }
        }

        // https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/api/subscribedsku_list
        private async Task<Tuple<string, string>> GetOfficeSkusAsync()
        {
            HttpResponseMessage res = await manager.QuerySkusAsync();

            var skus = (JArray)JObject.Parse(await res.Content.ReadAsStringAsync())["value"];

            string studentSku = null;
            string teacherSku = null;

            foreach (var sku in skus)
            {
                if ((string)sku["skuPartNumber"] == "STANDARDWOFFPACK_FACULTY")
                {
                    teacherSku = (string)sku["skuId"];
                }
                if ((string)sku["skuPartNumber"] == "STANDARDWOFFPACK_STUDENT")
                {
                    studentSku = (string)sku["skuId"];
                }
            }

            return new Tuple<string, string>(teacherSku, studentSku);
        }
    }
}
