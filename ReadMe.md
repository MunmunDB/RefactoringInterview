[Based on code from ](https://www.devjoy.com/blog/legacy-code-katas/)

- Break the hard dependency on the `Console` object
- Get the password comparison feature under test
- Get the password validation feature under test
- Implement a well known encrytion approach
- Optional
    - Implement local saving of a user


Developer : This is a code based designed to implement a creation of user info with a encryption using SHSA + PBKDF2, a inbuild encryption in .NET. 
The code structure is based on CLEAN code with TDD approach. The funcationalities are very basic and more focus is to design the system as Client & Server. The Client is responsible to get the user info and encrypt the password. The server has the encryption logic and finally save the user info to a No SQL Db. The client is set up as Console App, there is a scope to imlement different interface to collect user data by implementing the IClientApplication interface in the RefactoringInterview.Core.Application Layer

