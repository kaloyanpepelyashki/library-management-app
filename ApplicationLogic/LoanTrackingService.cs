using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using LibraryManagementApp.Infrastructure;
    using LibraryManagementApp.Infrastructure.Models;
    using LibraryManagementApp.Models;
    
    namespace LibraryManagementApp.ApplicationLogic;
    
    public class LoanTrackingService : ILoanTrackingService
    {
        private readonly string _borrowedBooksFilePath;
        private List<BorrowedBooksData> _borrowedBooksData;
    
        public LoanTrackingService()
        {
            _borrowedBooksFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\var\borrowedbooks.json"));
            _borrowedBooksData = LoadBorrowedBooksData();
        }
    
        private List<BorrowedBooksData> LoadBorrowedBooksData()
        {
            try
            {
                var fileContent = FileOperator.ReadFile(_borrowedBooksFilePath);
                var data = JsonSerializer.Deserialize<List<BorrowedBooksData>>(fileContent);
                return data ?? new List<BorrowedBooksData>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading borrowed books data: {e.Message}");
                return new List<BorrowedBooksData>();
            }
        }
    
        public List<int> GetUserBorrowedBooks(int userId)
        {
            var borrowedBooks = _borrowedBooksData.FirstOrDefault(b => b.UserId == userId);
    
            if (borrowedBooks == null)
                throw new ArgumentException($"Member with ID {userId} not found");
    
            return borrowedBooks.BorrowedBookIds ?? new List<int>();
        }
    
        public List<Member> GetAllMembersWithBorrowedBooks()
        {
            return _borrowedBooksData
                .Where(b => b.BorrowedBookIds != null && b.BorrowedBookIds.Count > 0)
                .Select(b => new Member(b.UserId, string.Empty, "Member") { CurrentlyBorrowedBooks = b.BorrowedBookIds })
                .ToList();
        }
    
        public bool UserHasBorrowedBook(int userId, int bookId)
        {
            var borrowedBooks = _borrowedBooksData.FirstOrDefault(b => b.UserId == userId);
    
            if (borrowedBooks == null)
                return false;
    
            return borrowedBooks.BorrowedBookIds?.Contains(bookId) ?? false;
        }
    
        public int GetUserBorrowedBooksCount(int userId)
        {
            var borrowedBooks = _borrowedBooksData.FirstOrDefault(b => b.UserId == userId);
    
            if (borrowedBooks == null)
                throw new ArgumentException($"Member with ID {userId} not found");
    
            return borrowedBooks.BorrowedBookIds?.Count ?? 0;
        }
    }