using UralsibTaskTest.Entities.Abstraction;

namespace UralsibTaskTest.Entities;

public class Commit: BaseEntity<int>
{
    public required string Login { get; set; }
    public required string UserName { get; set; }
    public string? Repository { get; set; }
    public required string Sha { get; set; }
    public DateTimeOffset DateCreate { get; set; }
    public string? Message { get; set; }
    public string? Url { get; set; }
}