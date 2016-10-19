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
                new Client
                {
                    AllowAccessToAllScopes=true,
                    Flow=Flows.ClientCredentials,
                    ClientId="webapi",
                    ClientName="IdentitySample WebAPI Sample",
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Enabled=true,
                    //RedirectUris = new List<string>
                    //{
                    //    "https://localhost:801/"
                    //},
                    //PostLogoutRedirectUris = new List<string>
                    //{
                    //    "https://localhost:801/"
                    //},
                }
            };
        }
    }
}