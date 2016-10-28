using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class IdentityCallbackController : Controller
    {
        static string IdentityAuthorityUri = "https://identity-dev.com/identity";
        static string IdentityAuthorizeUri = IdentityAuthorityUri + "/connect/authorize";
        static string BaseAddress = "https://identity-dev.com";
        static string IdentityTokenUri = IdentityAuthorityUri + "/connect/token";
        static string IdentitySTSCallbackUri = BaseAddress + "/IdentityCallback";

        // GET: IdentityCallback
        public async Task<ActionResult> Index()
        {
            var authCode = Request.QueryString["code"];
            var client = new TokenClient(IdentityTokenUri, "identitycode", "secret");

            var tokenResponse = await client.RequestAuthorizationCodeAsync(authCode, IdentitySTSCallbackUri);

            Response.Cookies["IdentitySample"]["access_token"] = tokenResponse.AccessToken;

            return Redirect(Request.QueryString["state"]);
        }

    }
}