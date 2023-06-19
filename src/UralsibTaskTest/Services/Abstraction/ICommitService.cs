using UralsibTaskTest.Entities;

namespace UralsibTaskTest.Services.Abstraction;

public interface ICommitService
{
    Task<IList<Commit>> AddCommitsAsync(IList<Commit> commits);
    Task<IList<Commit>> UpdateCommitsInRepositoryAsync(IList<Commit> commits,string login,string repository);
    Task<IList<Commit>> UpdateCommitsInUserAsync(IList<Commit> commits,string login);
    Task<IList<Commit>> GetCommitsInUserAndRepositoryAsync(string login, string repository, int count, int startIndex);
    Task<IList<Commit>> GetCommitsInUserAsync(string login, int count, int startIndex);
    Task DeleteCommitsAsync(IList<int> commitsId);
    Task<bool> ContainsCommitsInUserAsync(string login);
    Task<bool> ContainCommitsInUserAndRepositoryAsync(string login, string repository);
}