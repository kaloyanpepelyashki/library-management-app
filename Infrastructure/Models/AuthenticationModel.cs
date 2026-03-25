using System.Text.Json.Serialization;

namespace LibraryManagementApp.Infrastructure.Models;

public class AuthenticationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Username { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}