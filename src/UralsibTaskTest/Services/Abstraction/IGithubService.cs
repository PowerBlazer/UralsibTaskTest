using UralsibTaskTest.DTOs;

namespace UralsibTaskTest.Services.Abstraction;

public interface IGithubService
{
    Task<IList<RepositoryDto>?> GetRepositoriesInUserAsync(string login);
    Task<IList<CommitDto>?> GetCommitsInUserAndRepositoryAsync(string login, string repository);
    Task<IList<CommitDto>> GetCommitsAllGroupInUserAsync(string login);
}