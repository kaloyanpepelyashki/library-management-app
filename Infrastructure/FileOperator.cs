using System;
using System.IO;
using LibraryManagementApp.ApplicationLogic.Exceptions;

namespace LibraryManagementApp.Infrastructure;

public static class FileOperator
{

    public static string ReadFile(string filePath)
    {
        try
        {
            using StreamReader readr = new StreamReader(filePath);
            return readr.ReadToEnd();

        }
        catch(DirectoryNotFoundException e)
        {
            throw new FileOperationException($"Error reading file in FileOperator: File not found, {e.Message}");
        }
        catch (Exception e)
        {
            throw new FileOperationException($"$Error reading file in FileOperator: {e.Message}");;
        }
    }

    public static void WriteFile(string filePath, string content)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }
        catch(DirectoryNotFoundException e)
        {
            throw new FileOperationException($"Error writing file in FileOperator: File not found, {e.Message}");
        }
        catch (Exception e)
        {
            throw new FileOperationException($"$Error writing file in FileOperator: {e.Message}");
        }
    }
    
}