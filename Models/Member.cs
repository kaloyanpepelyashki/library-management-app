using System.Collections.Generic;

namespace LibraryManagementApp.Models;

public class Member : GeneralUser
{
    public Member(int id, string name, string role) : base(id, name, role)
    {
        BorrowLimit = 5;
    }
    
    
    public List<int> CurrentlyBorrowedBooks { get; set; }
    /// <summary>
    /// Keeps track of the borrowing history of a member. If false, the book hasn't been returned yet.
    /// </summary>
    public Dictionary<int, bool> BorrowHistory { get; set; }

    public bool ReturnBooK(int bookId)
    {
        return true;
    }

    public bool BorrowBook(int bookId)
    {
        return true;
    }

    public bool RateBook(int bookId)
    {
        return true;
    }
}