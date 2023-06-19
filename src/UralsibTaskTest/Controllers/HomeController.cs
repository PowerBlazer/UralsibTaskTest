using System.Diagnostics;
using UralsibTaskTest.Contexts.Abstraction;
using Microsoft.AspNetCore.Mvc;
using UralsibTaskTest.Models;
using UralsibTaskTest.Services.Abstraction;

namespace UralsibTaskTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGithubService _githubService;
    private readonly ICommitService _commitService;
    
    public HomeController(ILogger<HomeController> logger,
        IGithubService githubService, ICommitService commitService)
    {
        _logger = logger;
        _githubService = githubService;
        _commitService = commitService;
    }
    
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}