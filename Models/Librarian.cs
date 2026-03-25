namespace LibraryManagementApp.Models;

public class Librarian: GeneralUser
{
    public Librarian(int id, string name, string role) : base(id, name, role)
    {
        BorrowLimit = 10;
    }
    
}