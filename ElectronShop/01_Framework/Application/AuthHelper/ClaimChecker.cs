using Microsoft.AspNetCore.Http;

namespace _01_Framework.Application.AuthHelper
{
    public class ClaimChecker
    {
        public static bool CheckShopManagementClaim()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "ShopManagement")?.Value;
            if (claim == "True")
                return true;
            return false;
        }

        public static bool CheckInventoryManagementClaim()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "InventoryManagement")?.Value;
            if (claim == "True")
                return true;
            return false;
        }

        public static bool CheckCommentManagementClaim()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "CommentManagement")?.Value;
            if (claim == "True")
                return true;
            return false;
        }

        public static bool CheckDiscountManagementClaim()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "DiscountManagement")?.Value;
            if (claim == "True")
                return true;
            return false;
        }

        public static bool CheckAccountManagementClaim()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claim = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountManagement")?.Value;
            if (claim == "True")
                return true;
            return false;
        }
    }
}
