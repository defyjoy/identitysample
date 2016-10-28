using IdentityModel.Client;
using IdentitySample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        static string IdentityAuthorityUri = "https://identity-dev.com/identity";
        static string IdentityAuthorizeUri = IdentityAuthorityUri + "/connect/authorize";
        static string BaseAddress = "https://identity-dev.com";
        static string IdentityTokenUri = IdentityAuthorityUri + "/connect/token";
        static string IdentitySTSCallbackUri = BaseAddress + "/IdentityCallback";
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public HttpClient SetClientDefaults(Func<string> CredentialMethod)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress),
            };

            string accessToken = CredentialMethod();
            client.SetBearerToken(accessToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private string GetAccessTokenFromCookie()
        {
            var cookie = HttpContext.Request.Cookies.Get("IdentitySample");
            if (cookie != null && cookie["access_token"] != null)
            {
                return cookie["access_token"];
            }
            return string.Empty;
        }

        private void ClearCookie()
        {
            HttpContext.Request.Cookies.Remove("IdentitySample");
        }


        [Authorize]
        public async Task<ActionResult> ImplicitFlow()
        {

            HttpClient client = SetClientDefaults(RequestTokenAccessCredentials);
            HttpResponseMessage response = await client.GetAsync("api/employee/all");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(content);
                return View("employees", employees);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private string RequestTokenAccessCredentials()
        {
            ClearCookie();
            string accessToken = GetAccessTokenFromCookie();
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;

            var tokenClient = new TokenClient(IdentityTokenUri, "webapi", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;

            HttpContext.Response.Cookies["IdentitySample"]["access_token"] = tokenResponse.AccessToken;
            return tokenResponse.AccessToken;
        }

        [Authorize]
        public async Task<ActionResult> AuthCodeFlow()
        {
            HttpClient client = SetClientDefaults(RequestTokenAccessAuthorizationCode);
            HttpResponseMessage response = await client.GetAsync("api/employee/managers");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(content);
                return View("Employees", employees);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private string RequestTokenAccessAuthorizationCode()
        {
            string accessToken = GetAccessTokenFromCookie();
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;

            AuthorizeRequest authorizeRequest = new AuthorizeRequest(IdentityAuthorizeUri);
            var state = HttpContext.Request.Url.OriginalString;

            var url = authorizeRequest.CreateAuthorizeUrl("identitycode", "code", "api", IdentitySTSCallbackUri, state);

            HttpContext.Response.Redirect(url);
            return null;
        }
    }
}