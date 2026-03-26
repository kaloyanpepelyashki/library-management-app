using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryManagementApp.ApplicationLogic;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.Models;
using LibraryManagementApp.Views;

namespace LibraryManagementApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private ICatalogueService _catalogueService;
    
    private bool _hasUserAutenticated;
    private bool _isUserLibrarian = false;
    private IAuthenticationService _authenticationService;

    public ICommand LogoutCommand { get; }
    
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

    public MainWindowViewModel(ICatalogueService catalogueService, IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        _hasUserAutenticated = Session.GetAuthenticationCompleted();
        CheckIfUserLibrarian();
        //Tracks an event of session property change
        Session.AuthenticationChanged += OnAuthenticationChanged;
        Session.CurrentUserUpdate += CheckIfUserLibrarian;
        _catalogueService = catalogueService;
        _books = new ObservableCollection<Book>(_catalogueService.GetAllBooks());
        LogoutCommand = new RelayCommand(OnLogout);
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
    private void OnLogout()
    {
        _authenticationService.Logout();
        ShowLoginPopup();
    }
    private async void ShowLoginPopup()
    {
        var popup = new PopUpLogin();
        var mainWindow = Avalonia.Application.Current?.ApplicationLifetime
            is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow as MainWindow
            : null;

        if (mainWindow != null)
        {
            await popup.ShowDialog(mainWindow);
        }
    }
}
