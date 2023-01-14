namespace ecommerce.Helpers
{
    public interface IAppSession
    {
        string AuthorizationToken { get; }
        string UserName { get; }
    }
}
