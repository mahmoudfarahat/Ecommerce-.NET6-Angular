namespace ecommerce.Helpers
{
    public class AppSession : IAppSession
    {
        protected IHttpContextAccessor _httpContextAccessor;

        public AppSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string AuthorizationToken
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Request.Headers["Authorization"].Any())
                {
                    return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //public string UserName => _httpContextAccessor?.HttpContext?.User.;
    }
}
