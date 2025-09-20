using System.Security.Claims;

namespace ProductMicroService.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetRole(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.Role)?.Value;

        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

    }
}
