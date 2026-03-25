using System;

namespace LibraryManagementApp.ApplicationLogic.Exceptions;

public class ParsedDataEmptyException : Exception
{
    public ParsedDataEmptyException(string message) : base(message)
    {
    }

    public ParsedDataEmptyException(string message, Exception inner) : base(message, inner)
    {
    }
}