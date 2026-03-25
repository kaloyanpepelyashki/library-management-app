using System;

namespace LibraryManagementApp.Models;

public class GeneralUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Role AssignedRole { get; set; }
    public int BorrowLimit { get; set; }

    public GeneralUser(int id, string name, string role)
    {
        Id = id;
        Name = name;
        AssignedRole = Enum.TryParse<Role>(role, out Role result) ? result : throw new ArgumentException("Invalid role");
    }
}