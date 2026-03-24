namespace LibraryManagementApp.ViewModels;

public partial class PopUpLogin :  ViewModelBase
{
    private bool _isSignUp = false;

    public bool IsSignUp
    {
        get { return _isSignUp; }
        set
        {
            SetProperty(ref _isSignUp, value);
        }
    }
    
    public PopUpLogin() 
}