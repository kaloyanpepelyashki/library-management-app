using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementApp.ApplicationLogic.Interfaces;
using LibraryManagementApp.Infrastructure.Models;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic;

public class CatalogueService : ICatalogueService
{
    public CatalogueService()
    {
        
    }

    public List<Book> GetAllBooks()
    {
        try
        {
            List<BookPersistence>  booksPersistence = CatalogueServiceHelper.ParseBooks();
            
            if (booksPersistence.Count == 0)
            {
                Console.WriteLine("No books found");
                return new List<Book>();
            }
            
            List<Book> books = booksPersistence.Select(book => new Book
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                ISBN = book.Isbn,
                
            }).ToList();
            
            return books;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}