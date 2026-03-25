using System;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public static class Session
{
    private static GeneralUser CurrentUser { get; set; }
    private static bool AuthenticationCompleted { get; set; }
    public static event Action? AuthenticationChanged;
    public static event Action? CurrentUserUpdate;

    public static void SetCurrentUser(GeneralUser user)
    {
        CurrentUser = user;
        CurrentUserUpdate?.Invoke();
    }

    public static Role? GetCurrentUserRole()
    {
        return CurrentUser?.AssignedRole;
    }

    public static void SetAuthenticationCompleted()
    {
        AuthenticationCompleted = true;
        AuthenticationChanged?.Invoke();
    }

    public static bool GetAuthenticationCompleted()
    {
        return AuthenticationCompleted;
    }
}