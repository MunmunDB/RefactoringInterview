[Based on code from ](https://www.devjoy.com/blog/legacy-code-katas/)

- Break the hard dependency on the `Console` object
- Get the password comparison feature under test
- Get the password validation feature under test
- Implement a well known encrytion approach
- Optional
    - Implement local saving of a user


Developer : This is a code based designed to implement a creation of user info with a encryption using SHSA + PBKDF2, a inbuild encryption in .NET. 
The code structure is based on CLEAN code with TDD approach. The funcationalities are very basic and more focus is to design the system as Client & Server. The Client is responsible to get the user info and encrypt the password. The server has the encryption logic and finally save the user info to a No SQL Db. The client is set up as Console App, there is a scope to imlement different interface to collect user data by implementing the IClientApplication interface in the RefactoringInterview.Core.Application Layer

Key Concepts => 
Dependency Rule: Inner layers (Domain, Application) never depend on outer layers (Infrastructure, Presentation).

SOLID Principles: Interfaces like IClientApplication allow for extensibility and decoupling.

Testability: Logic is isolated and testable via the Unit Test layer.

Extensibility: You can easily swap the console app with a web or mobile client by implementing the same interface.

Pre-Requisities => .NET Core with target version 8.0 i.e Visual Studio 2022 very least


+-------------------------------------------------------------+
|                         Presentation Layer                  |
|                                                             |
|   - RefactoringInterview (Console App)                      |
|   - Handles user input/output                               |
|   - Depends on Application Layer interfaces                 |
+-------------------------------------------------------------+
                            ↓
+-------------------------------------------------------------+
|                      Application Layer                      |
|                                                             |
|   - RefactoringInterview.Core.Application                   |
|   - Contains use cases and business logic                   |
|   - Defines IClientApplication interface                    |
|   - No direct dependency on infrastructure                  |
+-------------------------------------------------------------+
                            ↓
+-------------------------------------------------------------+
|                      Domain Layer (Core)                    |
|                                                             |
|   - RefactoringIntreview.Core                               |
|   - Contains domain models and core entities                |
|   - Encryption logic (e.g., SHA + PBKDF2)                   |
|   - Pure C# classes, no external dependencies               |
+-------------------------------------------------------------+
                            ↓
+-------------------------------------------------------------+
|                    Infrastructure Layer                     |
|                                                             |
|   - RefactoringInterview.Infrastructure                     |
|   - Implements data access (NoSQL DB)                       |
|   - Provides concrete services for encryption & storage     |
|   - Depends on Application & Domain interfaces              |
+-------------------------------------------------------------+
                            ↓
+-------------------------------------------------------------+
|                          Test Layer                         |
|                                                             |
|   - RefactoringInterview.Tests.Unit                         |
|   - Unit tests for core logic and use cases                 |
|   - Follows TDD principles                                  |
+-------------------------------------------------------------+

