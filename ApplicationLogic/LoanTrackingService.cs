using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LibraryManagementApp.Infrastructure;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public class LoanTrackingService : ILoanTrackingService
{
    private readonly string _booksStorageFilePath;
    private List<Book> _booksData;

    public LoanTrackingService()
    {
        _booksStorageFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\var\book.storage.json"));
        _booksData = LoadBooksData();
    }

    private List<Book> LoadBooksData()
    {
        try
        {
            var fileContent = FileOperator.ReadFile(_booksStorageFilePath);
            var data = JsonSerializer.Deserialize<List<Book>>(fileContent);
            return data ?? new List<Book>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading books data: {e.Message}");
            return new List<Book>();
        }
    }

    public List<int> GetUserBorrowedBooks(int userId)
    {
        // Get all books that are marked as borrowed
        var borrowedBookIds = _booksData
            .Where(b => b.IsBorrowed)
            .Select(b => b.Id)
            .ToList();

        if (borrowedBookIds.Count == 0)
            throw new ArgumentException($"No borrowed books found");

        return borrowedBookIds;
    }

    public List<Member> GetAllMembersWithBorrowedBooks()
    {
        // Since we don't have user-specific borrowing data in book.storage.json,
        // we create a member with all borrowed books
        var borrowedBooks = _booksData
            .Where(b => b.IsBorrowed)
            .Select(b => b.Id)
            .ToList();

        if (borrowedBooks.Count == 0)
            return new List<Member>();

        var member = new Member(1, "All Members", "Member") 
        { 
            CurrentlyBorrowedBooks = borrowedBooks 
        };

        return new List<Member> { member };
    }

    public bool UserHasBorrowedBook(int userId, int bookId)
    {
        var book = _booksData.FirstOrDefault(b => b.Id == bookId);

        if (book == null)
            return false;

        return book.IsBorrowed;
    }

    public int GetUserBorrowedBooksCount(int userId)
    {
        return _booksData.Count(b => b.IsBorrowed);
    }
}

