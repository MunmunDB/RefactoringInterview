using RefactoringInterview.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringInterview.Core
{
    public class ErrorHandlingSecurityManager : ISecurityManager
    {
        public readonly ISecurityManager _innerSecurityManager;
         public ErrorHandlingSecurityManager(ISecurityManager innerSecurityManager)
        {
            _innerSecurityManager = innerSecurityManager;
        }
        public void CreateUser()
        {
            try
            {
                _innerSecurityManager.CreateUser();
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not implemented here)
                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
            }
        }
    }
}
