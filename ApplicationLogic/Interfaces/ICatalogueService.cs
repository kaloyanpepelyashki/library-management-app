using LibraryManagementApp.Models;
using System.Collections.Generic;
namespace LibraryManagementApp.ApplicationLogic.Interfaces;


public interface ICatalogueService
{
    List<Book> GetAllBooks();
}