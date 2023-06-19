using System.Net;
using System.Text.Json;
using UralsibTaskTest.DTOs;
using UralsibTaskTest.Services.Abstraction;

namespace UralsibTaskTest.Services;

public class GithubService: IGithubService
{
    private readonly HttpClient _httpClient;

    public GithubService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IList<RepositoryDto>?> GetRepositoriesInUserAsync(string login)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.github.com/users/{login}/repos"),
        };
        
        requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
        
        var response = await _httpClient.SendAsync(requestMessage);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Ошибка,повторите попытку позже");
        }

        var repositoriesJson = await response.Content.ReadAsStringAsync();
        var repositories = JsonSerializer.Deserialize<IList<RepositoryDto>>(repositoriesJson);

        return repositories;
    }

    public async Task<IList<CommitDto>?> GetCommitsInUserAndRepositoryAsync(string login, string repository)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.github.com/repos/{login}/{repository}/commits"),
        };
        
        requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
        
        var response = await _httpClient.SendAsync(requestMessage);
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Ошибка,повторите попытку позже");
        }

        var commitsJson = await response.Content.ReadAsStringAsync();
        var commitsDto = JsonSerializer
            .Deserialize<IList<CommitDto>>(commitsJson)?
            .Select(p =>
            {
                p.Repository = repository;
                p.Login = login;
                return p;
            })
            .ToList();

        return commitsDto;
    }

    public async Task<IList<CommitDto>> GetCommitsAllGroupInUserAsync(string login)
    {
        var repositories = await GetRepositoriesInUserAsync(login);
        
        var commitsGroup = repositories?.Select(async p =>
            await GetCommitsInUserAndRepositoryAsync(login, p.Name));

        var result = new List<CommitDto>();

        foreach (var commitGroupItem in commitsGroup!)
        {
            var commits = await commitGroupItem;
            if (commits is not null)
            {
                result.AddRange(commits);
            }
        }
        
        return result;
    }
}