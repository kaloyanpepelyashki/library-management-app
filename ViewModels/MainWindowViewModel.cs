using System;
using LibraryManagementApp.ApplicationLogic.Interfaces;

namespace LibraryManagementApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    private ICatalogueService _catalogueService;

    public MainWindowViewModel(ICatalogueService catalogueService)
    {
        _catalogueService = catalogueService;

        var books = catalogueService.GetAllBooks();
        
        Console.WriteLine(books);
    }
}