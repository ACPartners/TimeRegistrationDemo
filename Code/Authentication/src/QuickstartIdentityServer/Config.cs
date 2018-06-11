// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Polly.Wrap;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System;
using Polly;
using Polly.Timeout;
using Polly.Retry;
using Polly.Fallback;

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
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RedirectUris = { "http://localhost:3000/swagger/oauth2-redirect.html", "http://localhost/TimeRegistrationClient" },
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
            //retry untill request succeeds
            do
            {
                HttpResponseMessage response = await client.GetAsync("http://timeregistrationdemo/api/user");
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsJsonAsync<GetAllUsersForAuthenticationOutputDto>();
                }else
                {
                    Thread.Sleep(5000);
                }

            } while (users == null);
            return users;
        }


        public static List<TestUser> GetUsers()
        {
            //Retry untill other side return users successfull,
            //After 3 tries, just return John Doe...
            //Fallback policy -> create John Doe
            PolicyWrap<List<TestUser>> policy = CreateWaitPolicy();
            var users = GetTimeRegistrationDemoUsers();
            users.Wait(50000);

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

        private static PolicyWrap<List<TestUser>> CreateWaitPolicy()
        {
            var fallBackValue = new List<TestUser>();
            fallBackValue.Add(new TestUser { Username = "John", Password = "Doe", IsActive = true
        });
            int retries = 0;
            // Define our timeout policy: time out after 50 seconds.  We will use the pessimistic timeout strategy, which forces a timeout - even when the underlying delegate doesn't support it.
            var timeoutPolicy = Policy
                .Timeout(TimeSpan.FromSeconds(50), TimeoutStrategy.Pessimistic);

            // Define our waitAndRetry policy: keep retrying with 4 second gaps.  This is (intentionally) too long: to demonstrate that the timeout policy will time out on this before waiting for the retry.
            RetryPolicy waitAndRetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryForever(
                attempt => TimeSpan.FromSeconds(4),
                (exception, calculatedWaitDuration) =>
                {
                    Console.WriteLine(".Log,then retry: " + exception.Message);
                    retries++;
                });

            // Define a fallback policy: provide a nice substitute message to the user, if we found the call was rejected due to the timeout policy.
            FallbackPolicy<List<TestUser>> fallbackForTimeout = Policy<List<TestUser>>
                .Handle<TimeoutRejectedException>()
                .Fallback(
                    fallbackValue: fallBackValue,
                    onFallback: b =>
                    {
                        //Handle Exception information...
                        Console.WriteLine(b.Exception.Message);
                        return ;
                    }
                );

 
            // Compared to previous demo08: here we use *instance* wrap syntax, to wrap all in one go.
            PolicyWrap<List<TestUser>> policyWrap = fallbackForTimeout.Wrap(timeoutPolicy).Wrap(waitAndRetryPolicy);

            return policyWrap;
        }
    }
}