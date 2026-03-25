using System;
using LibraryManagementApp.ApplicationLogic.Exceptions;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.Infrastructure.Models;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public class AuthenticationService: IAuthenticationService
{

    public AuthenticationService()
    {
    }

    public bool Login(string username, string password)
    {
        try
        {
            (bool, AuthenticationModel) loginValidity = AuthenticationServiceHelper.ValidateLogin(username, password);

            if (!loginValidity.Item1) return false;

            var currentUser = new GeneralUser(loginValidity.Item2.Id, loginValidity.Item2.Username,
                loginValidity.Item2.Role);

            Session.SetCurrentUser(currentUser);

            return true;
        }
        catch (ParsedDataEmptyException e)
        {
            Console.WriteLine("Error in AuthenticationService data read from file appeared empty. Login flow Failed");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in AuthenticationService " + e.Message);
            throw;
        }
    }

    public bool SignUp(string username, string password, string role)
    {
        try
        {
            bool userExists = AuthenticationServiceHelper.IsUserAlreadyRegistered(username);
            if (userExists)
            {
                throw new UserAlreadyExistsException("User already exists");
            }

            AuthenticationServiceHelper.RegisterUserEntry(username, password, role);

            return true;
        }
        catch (UserAlreadyExistsException e)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in AuthenticationService. Error Signing up new user { e.Message}");
            throw;
        }
    }
}