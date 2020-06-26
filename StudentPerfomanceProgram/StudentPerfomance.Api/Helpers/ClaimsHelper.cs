using System.Security.Claims;

namespace StudentPerfomance.Api.Helpers
{
    internal static class ClaimsHelper
    {
        internal static int? GetIdentifier(ClaimsPrincipal claims)
        {
            if (int.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out int user_id))
                return user_id;

            return null;
        }
    }
}
