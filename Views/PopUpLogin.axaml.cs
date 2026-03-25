using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibraryManagementApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementApp.Views;

public partial class PopUpLogin : Window
{
    public PopUpLogin()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<PopUpLoginViewModel>();
    }
    
}