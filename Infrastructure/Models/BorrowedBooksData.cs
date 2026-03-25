using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryManagementApp.Infrastructure.Models;

public class BorrowedBooksData
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("borrowedBookIds")]
    public List<int> BorrowedBookIds { get; set; } = new();
}