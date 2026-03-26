using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryManagementApp.ApplicationLogic;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ViewModels;

public class LibrarianManagementViewModel : ViewModelBase
{
    private ILoanTrackingService _loanTrackingService;
    private ObservableCollection<Member> _membersWithBorrowedBooks;
    private string _selectedUserId;
    private string _selectedBookId;
    private bool _userHasBorrowedBook;

    public ObservableCollection<Member> MembersWithBorrowedBooks
    {
        get { return _membersWithBorrowedBooks; }
    }

    public string SelectedUserId
    {
        get { return _selectedUserId; }
        set { SetProperty(ref _selectedUserId, value); }
    }

    public string SelectedBookId
    {
        get { return _selectedBookId; }
        set { SetProperty(ref _selectedBookId, value); }
    }

    public bool UserHasBorrowedBook
    {
        get { return _userHasBorrowedBook; }
        set { SetProperty(ref _userHasBorrowedBook, value); }
    }

    public ICommand GetMembersCommand { get; }
    public ICommand CheckUserBorrowedBookCommand { get; }
    public ICommand GetUserBorrowedBooksCountCommand { get; }

    public LibrarianManagementViewModel(ILoanTrackingService loanTrackingService)
    {
        _loanTrackingService = loanTrackingService;
        _membersWithBorrowedBooks = new ObservableCollection<Member>();
        GetMembersCommand = new RelayCommand(LoadMembers);
        CheckUserBorrowedBookCommand = new RelayCommand(CheckUserBorrowedBook);
        GetUserBorrowedBooksCountCommand = new RelayCommand(GetUserBorrowedBooksCount);
    }

    private void LoadMembers()
    {
        try
        {
            var members = _loanTrackingService.GetAllMembersWithBorrowedBooks();
            _membersWithBorrowedBooks.Clear();
            foreach (var member in members)
            {
                _membersWithBorrowedBooks.Add(member);
            }
            Console.WriteLine("Members loaded successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading members: {ex.Message}");
        }
    }

    private void CheckUserBorrowedBook()
    {
        try
        {
            if (string.IsNullOrEmpty(SelectedUserId) || string.IsNullOrEmpty(SelectedBookId))
            {
                Console.WriteLine("Please enter both User ID and Book ID");
                return;
            }

            if (!int.TryParse(SelectedUserId, out int userId) || !int.TryParse(SelectedBookId, out int bookId))
            {
                Console.WriteLine("User ID and Book ID must be valid numbers");
                return;
            }

            UserHasBorrowedBook = _loanTrackingService.UserHasBorrowedBook(userId, bookId);
            Console.WriteLine($"User {userId} has borrowed book {bookId}: {UserHasBorrowedBook}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking borrowed book: {ex.Message}");
        }
    }

    private void GetUserBorrowedBooksCount()
    {
        try
        {
            if (string.IsNullOrEmpty(SelectedUserId))
            {
                Console.WriteLine("Please enter User ID");
                return;
            }

            if (!int.TryParse(SelectedUserId, out int userId))
            {
                Console.WriteLine("User ID must be a valid number");
                return;
            }

            int count = _loanTrackingService.GetUserBorrowedBooksCount(userId);
            Console.WriteLine($"User {userId} has borrowed {count} books");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting borrowed books count: {ex.Message}");
        }
    }
}