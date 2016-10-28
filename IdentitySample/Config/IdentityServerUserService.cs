using IdentityServer3.Core.Services.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using System.Threading.Tasks;

namespace IdentitySample.Config
{
    public class IdentityServerUserService : UserServiceBase
    {
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {

            return base.AuthenticateLocalAsync(context);
        }
    }
}