using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryManagementApp.ApplicationLogic.Exceptions;
using LibraryManagementApp.Infrastructure;
using LibraryManagementApp.Infrastructure.Models;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.ApplicationLogic.Interfaces;

public static class CatalogueServiceHelper
{
    private static readonly string _filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\var\book.storage.json"));

    public static List<BookPersistence> ParseBooks()
    {
        try
        {
            var booksTextString = FileOperator.ReadFile(_filePath);
            List<BookPersistence> bookPersistenceList =
                JsonSerializer.Deserialize<List<BookPersistence>>(booksTextString);

            if (bookPersistenceList == null)
            {
                throw new ParsedDataEmptyException($"Error in ParseBooks method. Books list is null ");
            }

            return bookPersistenceList;
        }
        catch (ParsedDataEmptyException ex)
        {
            throw; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CatalogueServiceHelper. Exception: {ex.Message}");
            throw; 
        }
    }
}