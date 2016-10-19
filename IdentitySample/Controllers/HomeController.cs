using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAPI()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:801"),
            };

            string accessToken = RequestTokenAccessCredentials();
            client.SetBearerToken(accessToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/values");
            if (response.IsSuccessStatusCode)
            {
                return Json(await response.Content.ReadAsStringAsync());
            }
            return Content("Bad Request");
        }

        private string RequestTokenAccessCredentials()
        {
            var cookie = HttpContext.Request.Cookies.Get("IdentitySample");
            if (cookie != null && cookie["access_token"] != null)
            {
                return cookie["access_token"];
            }

            var tokenClient = new TokenClient("https://localhost:801/identity/connect/token", "webapi", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;

            HttpContext.Response.Cookies["IdentitySample"]["access_token"] = tokenResponse.AccessToken;
            return tokenResponse.AccessToken;
        }
    }
}