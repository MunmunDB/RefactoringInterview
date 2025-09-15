using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactoringInterview.Core;
using RefactoringInterview.Core.Application;
using RefactoringInterview.Core.Domain;
using RefactoringInterview.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RefactoringInterview
{
    public static class DependencyInjectionExtensions
    {
        private const string LiteDbSection = "LiteDb";
        private const string DatabasePathKey = "DatabasePath";
        private const string DefaultDatabasePath = "users.db";

        public static IServiceCollection AddRefactoringInterviewServices(this IServiceCollection services)
        {
            services.AddTransient<LiteDatabase>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var dbPath = config.GetSection(LiteDbSection)[DatabasePathKey] ?? DefaultDatabasePath;
                return new LiteDatabase(dbPath);
            });
            services.AddTransient<IUserRepository, LiteDbUserRepository>();
            services.AddTransient<IClientApplication, ConsoleClientApplication>();
            services.AddTransient<SecurityManager>();
            services.AddTransient<IPasswordManager, PasswordManager>();
            // Error Handling Decorator for ISecurityManager
            services.AddTransient<ISecurityManager>(sp =>
            {
                var inner = sp.GetRequiredService<SecurityManager>();
                return new ErrorHandlingSecurityManager(inner);
            });
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            // Dynamic plugin loading for IClientApplication implementations
            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);
            foreach (var dll in Directory.GetFiles(pluginPath, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(dll);
                var pluginType = assembly.GetTypes()
                    .FirstOrDefault(t => typeof(IClientApplication).IsAssignableFrom(t) && !t.IsInterface);

                if (pluginType != null)
                    services.AddTransient(typeof(IClientApplication), pluginType);
            }
            return services;
        }
    }
}