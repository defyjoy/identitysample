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
                    Type=ScopeType.Resource,
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Scope
                {
                    Name="employee",
                    DisplayName="All employees access this scope",
                    Type=ScopeType.Resource,
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Scope
                {
                    Name="manager",
                    DisplayName="Only Manager related resources",
                    Type=ScopeType.Resource,
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                }
            };
        }
    }
}