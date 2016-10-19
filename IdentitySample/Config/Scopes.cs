using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.Profile,
                StandardScopes.OpenId,
                new Scope
                {
                    Name="api",
                    DisplayName="IdentitySample WebAPI Scope",
                    Type=ScopeType.Resource
                    //ScopeSecrets = new List<Secret>
                    //{
                    //    new Secret("secret".Sha256())
                    //},
                },

            };
        }
    }
}