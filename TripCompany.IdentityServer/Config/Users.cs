using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
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
                    Subject = "a9d95793-9ece-4a5b-872e-438c60be44e4",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Taras"), 
                        new Claim(Constants.ClaimTypes.FamilyName, "Samkovskyi")
                    }
                },
                new InMemoryUser
                {
                    Username = "Oliver",
                    Password = "secret",
                    Subject = "f390bf37-a8dc-4872-80ed-2ac919b4ddb0",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Oliver"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Giroud")
                    }
                }
            };
        }
    }
}
