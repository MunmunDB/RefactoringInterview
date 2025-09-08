using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactoringInterview;
using RefactoringInterview.Core.Application;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddRefactoringInterviewServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve and use ISecurityManager
var securityManager = serviceProvider.GetRequiredService<ISecurityManager>();
securityManager.CreateUser();
Console.ReadKey();
