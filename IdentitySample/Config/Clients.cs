using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                // 1. Basic client credentials flow
                // 2. Used with confidential Clients
                // 3. Used with clients like WebAPI.
                
                new Client
                {
                    Flow=Flows.ClientCredentials,
                    AllowAccessToAllScopes=true,
                    ClientId="webapi",
                    ClientName="IdentitySample WebAPI Sample",
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Enabled=true
                },
                new Client
                {
                    Flow=Flows.Implicit,
                    AllowAccessToAllScopes=true,
                    AllowedScopes = new List<string> {
                        "employee"
                    },
                    ClientId="mvc",
                    ClientName="IdentitySample Mvc Client",
                    Enabled=true,
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = new List<string>
                    {
                        "https://identity-dev.com"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://identity-dev.com"
                    },
                },
                new Client
                {
                    Flow=Flows.AuthorizationCode,
                    AllowedScopes = new List<string> {
                        "manager"
                    },
                    RequireConsent=false,
                    ClientId="identitycode",
                    ClientName="IdentitySample Mvc client with authorization code",
                    Enabled=true,
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = new List<string>
                    {
                        "https://identity-dev.com/identitycallback"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://identity-dev.com"
                    },
                },

            };
        }
    }
}