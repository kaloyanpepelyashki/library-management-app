using System.IO;

namespace LibraryManagementApp.Infrastructure;

public class FileOperator
{
    public FileOperator()  {}

    public string ReadFile(string filePath)
    { 
        StreamReader readr = new StreamReader(filePath);
        var fileContent = readr.ReadToEnd();
        
        return fileContent;
    }
    
}