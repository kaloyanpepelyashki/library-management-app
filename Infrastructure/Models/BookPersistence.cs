using System.Text.Json.Serialization;

namespace LibraryManagementApp.Infrastructure.Models;

public class BookPersistence
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("author")]
    public string Author { get; set; }
    [JsonPropertyName("isbn")]
    public string Isbn { get; set; }
    [JsonPropertyName("isBorrowed")]
    public bool IsBorrowed { get; set; }
}