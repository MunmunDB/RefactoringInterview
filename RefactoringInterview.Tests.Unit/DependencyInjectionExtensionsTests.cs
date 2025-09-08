using System;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringInterview;
using RefactoringInterview.Core.Application;
using RefactoringInterview.Core.Domain;

public class DependencyInjectionExtensionsTests
{
    [TestMethod]
    public void AddRefactoringInterviewServices_RegistersLiteDatabase_WithConfiguredPath()
    {
        // Arrange
        var inMemorySettings = new System.Collections.Generic.Dictionary<string, string>
        {
            { "LiteDb:DatabasePath", "test-users.db" }
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Act
        services.AddRefactoringInterviewServices();
        var provider = services.BuildServiceProvider();

        // Assert
        var db = provider.GetRequiredService<LiteDatabase>();

        // The following line checks the database path via reflection (LiteDB does not expose it directly)
        var field = typeof(LiteDatabase).GetField("_engine", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var engine = field?.GetValue(db);
        var filenameProp = engine?.GetType().GetProperty("Filename");
        var filename = filenameProp?.GetValue(engine)?.ToString();

        // Use filename after it is declared
        Assert.IsTrue((filename ?? string.Empty).Contains("test-users.db"));
        Assert.IsNotNull(db);


        // Also check other services are registered
        Assert.IsNotNull(provider.GetRequiredService<IUserRepository>());
        // Use filename after it is declared
        Assert.IsTrue((filename ?? string.Empty).Contains("test-users.db"));
        Assert.IsNotNull(db);

        // Remove this line as Assert.Contains does not exist in MSTest
        // Assert.Contains("test-users.db", filename ?? string.Empty);

        // Instead, use Assert.IsTrue to check if the filename contains the expected value
        Assert.IsTrue((filename ?? string.Empty).Contains("test-users.db"));

        // Also check other services are registered
        Assert.IsNotNull(provider.GetRequiredService<IUserRepository>());
        Assert.IsNotNull(provider.GetRequiredService<IClientApplication>());
        Assert.IsNotNull(provider.GetRequiredService<ISecurityManager>());
        Assert.IsNotNull(provider.GetRequiredService<IPasswordManager>());
        Assert.IsNotNull(provider.GetRequiredService<IClientApplication>());
        Assert.IsNotNull(provider.GetRequiredService<ISecurityManager>());
        Assert.IsNotNull(provider.GetRequiredService<IPasswordManager>());
    }
}