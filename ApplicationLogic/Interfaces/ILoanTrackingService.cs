using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public interface ILoanTrackingService
{
    List<int> GetUserBorrowedBooks(int userId);
    List<Member> GetAllMembersWithBorrowedBooks();
    bool UserHasBorrowedBook(int userId, int bookId);
    int GetUserBorrowedBooksCount(int userId);
}