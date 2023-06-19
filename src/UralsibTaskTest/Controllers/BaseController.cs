using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace UralsibTaskTest.Controllers;

public class BaseController: Controller
{
    internal string? Login => User.FindFirstValue("urn:github:username");
    internal string? UserName => User.FindFirstValue("urn:github:name");
}