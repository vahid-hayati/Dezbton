namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        #region  Dependency Injection
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<IAccountRepository, AccountRepository>(); //Controller LifeCycle
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        #endregion Dependency Injection

        return services;
    }
}