using System;
using Avalonia.Controls;

namespace LibraryManagementApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        
        var popup = new PopUpLogin();
        await popup.ShowDialog(this);
    }
}