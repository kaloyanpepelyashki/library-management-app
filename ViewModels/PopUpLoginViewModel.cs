using System;
using System.Windows.Input;
using LibraryManagementApp.ApplicationLogic;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.Views;

namespace LibraryManagementApp.ViewModels;

public partial class PopUpLoginViewModel :  ViewModelBase
{
    IAuthenticationService _authenticationService;
    
    private string _username;
    private string _password;
    private bool _isLibrarian = false; 
    
    private bool _isSignUp = false;
    
    public bool IsSignUp
    {
        get { return _isSignUp; }
        set
        {
            if (SetProperty(ref _isSignUp, value))
            {
                OnPropertyChanged(nameof(IsSignIn));
            }
        }
    }

    public string Username
    {
        get { return _username; }
        set { SetProperty(ref _username, value); }
    }

    public string Password
    {
        get { return _password; }
        set { SetProperty(ref _password, value); }
    }

    public bool IsLibrarian
    {
        get { return _isLibrarian; }
        set { SetProperty(ref _isLibrarian, value); }
    }

    public bool IsSignIn => !IsSignUp;
    public bool IsNotLibrarian => !IsSignUp;
    
    public ICommand SetIsSignUpCommand { get; }
    public ICommand SetIsSignInCommand { get; }
    public ICommand SetIsLibrarianCommand { get; }
    public ICommand SetIsNotLibrarianCommand { get; }
    public ICommand OnSignInCommand { get; }
    public ICommand OnSignUpCommand { get; }

    public PopUpLoginViewModel(IAuthenticationService authenticationService)
    {
        SetIsSignUpCommand = new RelayCommand(() => IsSignUp = true);
        SetIsSignInCommand = new RelayCommand(() => IsSignUp = false);
        SetIsLibrarianCommand = new RelayCommand(() => IsLibrarian = true);
        SetIsNotLibrarianCommand = new RelayCommand(() => IsLibrarian = false);
        OnSignInCommand = new RelayCommand(OnSignIn);
        OnSignUpCommand = new RelayCommand(OnSignUp);
        _authenticationService = authenticationService;
    }

    public void OnSignIn()
    {
        try
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                Console.WriteLine("Username or password is empty");
                return;
            }
            
            bool authenticationResult = _authenticationService.Login(Username, Password);

            if (authenticationResult)
            {
                Console.WriteLine("Successfully logged in");
                return;
            }
            
            Console.WriteLine("Not Successfully logged in");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnSignIn(): {ex.Message}");
        }
    }

    public void OnSignUp()
    {
        Console.WriteLine($"Name : {Username} and Password : {Password}");
        Console.WriteLine($"IsLibrarian : {IsLibrarian}");
        try
        {
            var role = "Member";

            if (IsLibrarian)
            {
                role = "Librarian";
            }
            _authenticationService.SignUp(Username, Password, role);
        
            // Auto-login after sign-up
            _authenticationService.Login(Username, Password);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnSignUp(): {ex.Message}");
        }
    }
    
}