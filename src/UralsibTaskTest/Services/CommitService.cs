using Microsoft.EntityFrameworkCore;
using UralsibTaskTest.Contexts.Abstraction;
using UralsibTaskTest.Entities;
using UralsibTaskTest.Services.Abstraction;

namespace UralsibTaskTest.Services;

public class CommitService: ICommitService
{
    private readonly IDatabaseContext _databaseContext;

    public CommitService(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IList<Commit>> AddCommitsAsync(IList<Commit> commits)
    {
        await _databaseContext.Commits.AddRangeAsync(commits);
        await _databaseContext.SaveChangesAsync();
        
        return commits;
    }

    public async Task<IList<Commit>> UpdateCommitsInRepositoryAsync(IList<Commit> commits,string login,string repository)
    {
        var commitsBase = await _databaseContext.Commits
            .Where(p => p.Login == login || p.Repository == repository)
            .ToListAsync();

        _databaseContext.Commits.RemoveRange(commitsBase);
        await _databaseContext.SaveChangesAsync();

        return await AddCommitsAsync(commits);
    }

    public async Task<IList<Commit>> UpdateCommitsInUserAsync(IList<Commit> commits, string login)
    {
        var commitsBase = await _databaseContext.Commits
            .Where(p => p.Login == login)
            .ToListAsync();
        
        _databaseContext.Commits.RemoveRange(commitsBase);
        await _databaseContext.SaveChangesAsync();

        return await AddCommitsAsync(commits);
    }

    public async Task<IList<Commit>> GetCommitsInUserAndRepositoryAsync(string login, string repository, 
        int count, int startIndex)
    {
        var commits = await _databaseContext.Commits
            .Where(p=>p.Login == login && p.Repository == repository)       
            .Skip(startIndex)
            .Take(count)
            .ToListAsync();
        
        return commits;
    }

    public async Task<IList<Commit>> GetCommitsInUserAsync(string login, int count, int startIndex)
    {
        var commits = await _databaseContext.Commits
            .Where(p=>p.Login == login)       
            .Skip(startIndex)
            .Take(count)
            .ToListAsync();
        
        return commits;
    }

    public async Task DeleteCommitsAsync(IList<int> commitsId)
    {
        var commitsToDelete = await _databaseContext.Commits
            .Where(p => commitsId.Contains(p.Id))
            .ToListAsync();

        _databaseContext.Commits.RemoveRange(commitsToDelete);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<bool> ContainsCommitsInUserAsync(string login)
    {
        var countCommits = await _databaseContext.Commits
            .CountAsync(p => p.Login == login);

        return countCommits > 0;
    }

    public async Task<bool> ContainCommitsInUserAndRepositoryAsync(string login, string repository)
    {
        var countCommits = await _databaseContext.Commits
            .CountAsync(p => p.Login == login && p.Repository == repository);

        return countCommits > 0;
    }
}