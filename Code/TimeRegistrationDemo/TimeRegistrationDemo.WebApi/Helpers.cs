using System.Security.Claims;

namespace TimeRegistrationDemo.WebApi
{
    public static class Helpers
    {
        public static int GetUserIdFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirst(x => x.Type == "sub").Value.ToString()); ;
        }
    }
}
