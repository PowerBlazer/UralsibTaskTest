using System.Text.Json.Serialization;

namespace UralsibTaskTest.DTOs;

public class RepositoryDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}