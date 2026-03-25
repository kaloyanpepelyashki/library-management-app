using System;

namespace LibraryManagementApp.ApplicationLogic.Exceptions;

public class FileOperationException: Exception
{
    public FileOperationException(string message) :  base(message) { }
    public FileOperationException(string message, Exception inner) :  base(message, inner) { }
}