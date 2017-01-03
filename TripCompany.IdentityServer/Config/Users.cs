using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.InMemory;

namespace TripCompany.IdentityServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Taras",
                    Password = "secret",
                    Subject = "subject-hash-taras"
                },
                new InMemoryUser
                {
                    Username = "Oliver",
                    Password = "secret",
                    Subject = "subject-hash-oliver"
                }
            };
        }
    }
}
