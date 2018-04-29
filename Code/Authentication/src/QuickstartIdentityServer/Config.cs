// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuickstartIdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                new ApiResource("HolidayRequests", "Holiday Requests")
                {
                    UserClaims = new List<string>
                    {
                        "name",
                        "role"
                    }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "HolidayRequests" }
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://172.18.62.10/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { "" },
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "HolidayRequests",
                        "api1"
                    }
                },

                // PostMan testing
                new Client
                {
                    ClientId = "postman-api",
                    ClientName = "Postman Test Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    AllowedCorsOrigins = { "https://www.getpostman.com" },
                    EnableLocalLogin = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "HolidayRequests"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                }
            };
        }

        public static async Task<GetAllUsersForAuthenticationOutputDto> GetTimeRegistrationDemoUsers()
        {
            GetAllUsersForAuthenticationOutputDto users = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://dockercompose18376638957051028791_timeregistrationdemo_1/api/user");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsJsonAsync<GetAllUsersForAuthenticationOutputDto>();
            }
            return users;
        }


        public static List<TestUser> GetUsers()
        {
            var users = GetTimeRegistrationDemoUsers();
            users.Wait(5000);

            var testUsers = users.Result.Users.Select(x =>
                new TestUser
                {
                    SubjectId = x.Id.ToString(),
                    Username = x.FirstName,
                    Password = x.LastName,
                    Claims = new List<Claim>
                    {
                        new Claim("name",x.FirstName + " " + x.LastName),
                        new Claim("role", x.Roles.FirstOrDefault().Id)
                    }
                }
            ).ToList();

            return testUsers;

            //return new List<TestUser>
            //{
            //    new TestUser
            //    {
            //        SubjectId = "50",
            //        Username = "jef",
            //        Password = "jef",
            //        Claims = new List<Claim>
            //        {
            //            new Claim("name", "jef"),
            //            new Claim("role","Employee")
            //        }
            //    },

            //    new TestUser
            //    {
            //        SubjectId = "1",
            //        Username = "alice",
            //        Password = "password",

            //        Claims = new List<Claim>
            //        {
            //            new Claim("name", "alice"),
            //            new Claim("website", "https://alice.com"),
            //            new Claim("role","Administrators")
            //        }
            //    },
            //    new TestUser
            //    {
            //        SubjectId = "2",
            //        Username = "bob",
            //        Password = "password",

            //        Claims = new List<Claim>
            //        {
            //            new Claim("name", "bob"),
            //            new Claim("website", "https://bob.com"),
            //            new Claim("role","Managers")
            //        }
            //    }
            //};
        }
    }
}