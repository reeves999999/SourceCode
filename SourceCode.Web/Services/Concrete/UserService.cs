using Microsoft.AspNetCore.Http;

namespace SourceCode.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLoggedInUserId()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
