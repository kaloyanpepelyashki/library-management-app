using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LibraryManagementApp.ApplicationLogic.Exceptions;
using LibraryManagementApp.Infrastructure;
using LibraryManagementApp.Infrastructure.Models;

namespace LibraryManagementApp.ApplicationLogic;

public static class AuthenticationServiceHelper
{
    private static readonly string _filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\var\user.information.json"));

    public static (
        bool isLoginValid,
        AuthenticationModel? userModel
        ) ValidateLogin(string userName, string typedPassword)
    {
        try
        {
            var allUserDataText = FileOperator.ReadFile(_filePath);
            var allUsersParsed =
                JsonSerializer.Deserialize<List<AuthenticationModel>>(allUserDataText);

            if (allUsersParsed == null)
                throw new ParsedDataEmptyException("Parsed data is empty in AuthenticationServiceHelper");

            if (allUsersParsed.Count == 0) return (false, null);

            var authModel = allUsersParsed.FirstOrDefault(obj => obj.Username == userName);
            

            if (authModel == null) return (false, null);
            
            bool isPasswordValid = CheckIfPasswordMatch(typedPassword, authModel.Password);

            if (isPasswordValid)
            {
                return (true, authModel);
            }
            
            return (false, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Error validating login info in AuthenticationServiceHelper class: {e.Message}, {e.GetType()}");
            throw;
        }
    }

    private static bool CheckIfPasswordMatch(string typedPassword, string storedHash)
    {
        try
        {
            return PasswordHasher.VerifyPassword(typedPassword, storedHash);
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Error validating password in AuthenticationServiceHelper class: {e.Message}, {e.GetType()}");
            throw;
        }
    }

    public static bool IsUserAlreadyRegistered(string username)
    {
        try
        {
            
            var allUsersDataText = FileOperator.ReadFile(_filePath);
            var allUsersParsed = JsonSerializer.Deserialize<List<AuthenticationModel>>(allUsersDataText);

            if (allUsersParsed == null)
                throw new ParsedDataEmptyException("Parsed data is empty in AuthenticationServiceHelper");

            if (allUsersParsed.Count == 0) return false;

            var authModel = allUsersParsed.FirstOrDefault(obj => obj.Username == username);

            if (authModel == null) return false;

            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Error validating if user is already registered in AuthenticationServiceHelper class: {e.Message}, {e.GetType()}");
            throw;
        }
    }

    private static int GenerateId()
    {
        var allUsersDataText = FileOperator.ReadFile(_filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var allUsersParsed = string.IsNullOrWhiteSpace(allUsersDataText)
            ? new List<AuthenticationModel>()
            : JsonSerializer.Deserialize<List<AuthenticationModel>>(allUsersDataText, options)
              ?? new List<AuthenticationModel>();

        if (allUsersParsed.Count == 0)
        {
            return 1;
        }
        
        int greatestId = allUsersParsed.Max(obj => obj.Id);

        return greatestId + 1; 
    }
    
    public static bool RegisterUserEntry(string username, string password, string role)
    {
        try
        {
            var userFileContentString = File.ReadAllText(_filePath);
            List<AuthenticationModel> allUsersData =
                JsonSerializer.Deserialize<List<AuthenticationModel>>(userFileContentString);


            int userID = GenerateId();
            var hasedPasword = PasswordHasher.HashPassword(password);

            AuthenticationModel authModel = new AuthenticationModel
                { Id = userID, Username = username, Password = hasedPasword, Role = role };

            allUsersData.Add(authModel);

            var jsonString = JsonSerializer.Serialize(allUsersData);

            FileOperator.WriteFile(_filePath, jsonString);

            return true;
        }
        catch (FileOperationException e)
        {
            Console.WriteLine($"Error in AuthenticationServiceHelper. File operation exception: {e.Message}, {e.GetType()}");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error registering user entry in AuthenticationServiceHelper class: {e.Message}, {e.GetType()}");
            throw;
        }
    }
}