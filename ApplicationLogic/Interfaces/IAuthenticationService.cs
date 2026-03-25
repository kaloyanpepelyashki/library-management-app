namespace LibraryManagementApp.ApplicationLogic.Interfaces;

public interface IAuthenticationService
{
    bool Login(string username, string password);
    bool SignUp(string username, string password, string role);
}