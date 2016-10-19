using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;

namespace IdentitySample.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get() => new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Joydeep",
                    Password = "secret",
                    Subject = "1",
                }
            };
    }
}