using LibraryManagementApp.ApplicationLogic;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementApp;

public static class ServiceCollectionExtension
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<PopUpLoginViewModel>();
    }
}