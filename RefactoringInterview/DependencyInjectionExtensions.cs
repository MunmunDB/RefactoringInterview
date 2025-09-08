using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RefactoringInterview.Core.Domain;
using RefactoringInterview.Infrastructure;
using RefactoringInterview.Core.Application;

namespace RefactoringInterview
{
    public static class DependencyInjectionExtensions
    {
        private const string LiteDbSection = "LiteDb";
        private const string DatabasePathKey = "DatabasePath";
        private const string DefaultDatabasePath = "users.db";

        public static IServiceCollection AddRefactoringInterviewServices(this IServiceCollection services)
        {
            services.AddSingleton<LiteDatabase>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var dbPath = config.GetSection(LiteDbSection)[DatabasePathKey] ?? DefaultDatabasePath;
                return new LiteDatabase(dbPath);
            });
            services.AddScoped<IUserRepository, LiteDbUserRepository>();
            services.AddSingleton<IClientApplication, ConsoleClientApplication>();
            services.AddSingleton<ISecurityManager, SecurityManager>();
            services.AddSingleton<IPasswordManager, PasswordManager>();
           
            return services;
        }
    }
}