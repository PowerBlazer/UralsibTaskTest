using System.Text.Json.Serialization;
using UralsibTaskTest.Entities;

namespace UralsibTaskTest.DTOs;

public class CommitDto
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonPropertyName("sha")]
    public required string Sha { get; set; }
    [JsonPropertyName("commit")]
    public required CommitUser Commit { get; set; }
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    [JsonIgnore]
    public string? Repository { get; set; }
    [JsonIgnore]
    public string? Login { get; set; }
    
}

public class CommitUser
{
    [JsonPropertyName("author")]
    public required Author Author { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class Author
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }
}