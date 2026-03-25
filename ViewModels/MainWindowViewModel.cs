using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibraryManagementApp.ApplicationLogic;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private ICatalogueService _catalogueService;
    
    private bool _hasUserAutenticated;
    private bool _isUserLibrarian = false;
    
    private ObservableCollection<Book> _books;

    public ObservableCollection<Book> Books
    {
        get { return _books; }
        
    }

    public bool HasUserAutenticated
    {
        get { return _hasUserAutenticated; }
        set => SetProperty(ref _hasUserAutenticated, value);
    }

    public bool IsUserLibrarian
    {
        get { return _isUserLibrarian; }
        set => SetProperty(ref _isUserLibrarian, value);
    }

    public MainWindowViewModel(ICatalogueService catalogueService)
    {
        _hasUserAutenticated = Session.GetAuthenticationCompleted();
        CheckIfUserLibrarian();
        //Tracks an event of session property change
        Session.AuthenticationChanged += OnAuthenticationChanged;
        Session.CurrentUserUpdate += CheckIfUserLibrarian;
        _catalogueService = catalogueService;
        _books = new ObservableCollection<Book>(_catalogueService.GetAllBooks());
        
    }

    private void CheckIfUserLibrarian()
    {
        if (Session.GetCurrentUserRole() == null)
        {
            _isUserLibrarian = false;
            return;
        }
        
        if (Session.GetCurrentUserRole() == Role.Librarian)
        {
            _isUserLibrarian = true;
            Console.WriteLine("Librarian");
            return;
        }
        Console.WriteLine("Not Librarian");
        _isUserLibrarian = false;
    }
    
    private void OnAuthenticationChanged()
    {
        HasUserAutenticated = Session.GetAuthenticationCompleted();
    }
}
