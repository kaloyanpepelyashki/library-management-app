using System;
using LibraryManagementApp.Application.Exceptions;
using LibraryManagementApp.Infrastructure;
using LibraryManagementApp.Infrastructure.Models;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.Application;

public class AuthenticationService
{
    private string _authenticationFilePath;

    public AuthenticationService()
    {
        _authenticationFilePath = "user.information.json";
    }

    public bool Login(string username, string password)
    {
        try
        {
            (bool, AuthenticationModel) loginValidity = AuthenticationServiceHelper.ValidateLogin(username, password);

            if (!loginValidity.Item1)
            {
                return false; 
            }
            
            GeneralUser currentUser = new GeneralUser(loginValidity.Item2.Id, loginValidity.Item2.Username, loginValidity.Item2.Role);
            Session.SetCurrentUser(currentUser);

            return true; 
        }
        catch (ParsedDataEmptyException e)
        {
            Console.WriteLine($"Error in AuthenticationService data read from file appeared empty. Login flow Failed");
            throw;
        }
    }
}