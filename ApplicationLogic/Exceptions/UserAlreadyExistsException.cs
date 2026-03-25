using System;

namespace LibraryManagementApp.ApplicationLogic.Exceptions;

public class UserAlreadyExistsException: Exception
{
    public UserAlreadyExistsException(string message) : base(message) { }
}