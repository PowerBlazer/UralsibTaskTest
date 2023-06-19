using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UralsibTaskTest.Common;
using UralsibTaskTest.Services.Abstraction;

namespace UralsibTaskTest.Controllers;

[Authorize]
[Route("[controller]")]
public class CommitController: BaseController
{
    private readonly ICommitService _commitService;
    private readonly IGithubService _githubService;

    public CommitController(ICommitService commitService, 
        IGithubService githubService)
    {
        _commitService = commitService;
        _githubService = githubService;
    }
    
    [HttpGet("owner")]
    public async Task<IActionResult> CommitsUser(int page = 0,int count = 20)
    {
        if (!await _commitService.ContainsCommitsInUserAsync(Login!))
        {
            var commitsGroup = await _githubService.GetCommitsAllGroupInUserAsync(Login!);
            
            await _commitService.AddCommitsAsync(commitsGroup
                    .Select(CommitMapper.ToEntity).ToList());
        }
        
        var commitsBase = await _commitService
            .GetCommitsInUserAsync(Login!,count,page*10);

        var result = commitsBase
            .Select(CommitMapper.ToDto)
            .ToList();
            
        return View("~/Views/Commit/Owner.cshtml",result);
    }
    
    [HttpGet("user")]
    public async Task<IActionResult> Commits(string login, string repository,
        int page = 0, int count = 10)
    {
        if (!await _commitService.ContainCommitsInUserAndRepositoryAsync(login, repository))
        {
            var newCommits = await _githubService.GetCommitsInUserAndRepositoryAsync(login, repository);
            if (newCommits is not null)
            {
                await _commitService.AddCommitsAsync(newCommits
                    .Select(CommitMapper.ToEntity).ToList());
            }
        }

        var commits = await _commitService.GetCommitsInUserAndRepositoryAsync(login, repository,
            count, page * 10);
        
        var result = commits
            .Select(CommitMapper.ToDto)
            .ToList();

        return View("~/Views/Commit/User.cshtml", result);
    }

    [AllowAnonymous]
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteCommits([FromBody]IList<int> commitsId)
    {
        await _commitService.DeleteCommitsAsync(commitsId);

        return Ok();
    }

    [HttpGet("update-user")]
    public async Task<IActionResult> UpdateCommitsInUser()
    {
        var commitsUserDto = await _githubService
            .GetCommitsAllGroupInUserAsync(Login!);
        
        var commitsUser = commitsUserDto
            .Select(CommitMapper.ToEntity)
            .ToList();

        await _commitService.UpdateCommitsInUserAsync(commitsUser, Login!);

        return RedirectToAction("CommitsUser");
    }
    
    [HttpGet("update")]
    public async Task<IActionResult> UpdateCommits(string login, string repository)
    {
        var commitsDto = await _githubService
            .GetCommitsInUserAndRepositoryAsync(login, repository);
        
        var commits = commitsDto?.Select(CommitMapper.ToEntity)
            .ToList();

        await _commitService.UpdateCommitsInRepositoryAsync(commits!, login, repository);

        return RedirectToAction("Commits",new {repository = repository,login = login });
    }
}