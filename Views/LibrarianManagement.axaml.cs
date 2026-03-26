using Avalonia.Controls;
    using LibraryManagementApp.ViewModels;
    using Microsoft.Extensions.DependencyInjection;
    
    namespace LibraryManagementApp.Views;
    
    public partial class LibrarianManagement : UserControl
    {
        public LibrarianManagement()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<LibrarianManagementViewModel>();
        }
    }