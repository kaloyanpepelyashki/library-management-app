using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public static class Session
{
    private static GeneralUser CurrentUser { get; set; }

    public static void SetCurrentUser(GeneralUser user)
    {
        CurrentUser = user;
    }

    public static Role GetCurrentUserRole()
    {
        return CurrentUser.AssignedRole;
    }
}