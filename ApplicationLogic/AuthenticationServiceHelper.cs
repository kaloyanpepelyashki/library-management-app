using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using LibraryManagementApp.Application.Exceptions;
using LibraryManagementApp.Infrastructure;
using LibraryManagementApp.Infrastructure.Models;

namespace LibraryManagementApp.Application;

public static class AuthenticationServiceHelper
{
    private static string _filePath = "user.information.json";
    
    public static (
         bool isLoginValid,
        AuthenticationModel? userModel
    ) ValidateLogin(string userName, string password)
    {
        try
        {
            var allUserDataText = FileOperator.ReadFile(_filePath);
            List<AuthenticationModel> allUsersParsed =
                JsonSerializer.Deserialize<List<AuthenticationModel>>(allUserDataText);

            if (allUsersParsed == null)
            {
                throw new ParsedDataEmptyException("Parsed data is empty in AuthenticationServiceHelper");
            }

            if (allUsersParsed.Count == 0)
            {
                return (false, null);
            }

            AuthenticationModel authModel = allUsersParsed.FirstOrDefault(obj => obj.Username == userName && obj.Password == password);


            if (authModel == null)
            {
                return (false, null);
            }
            
            return (true, authModel) ;

        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Error validating login info in AuthenticationServiceHelper class: {e.Message}, {e.GetType()}");
            throw;
        }
    }
}