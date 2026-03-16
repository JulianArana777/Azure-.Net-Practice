using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvcApp.Models;
using API.Interface;

namespace DemoMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlobService _blob;

    public HomeController(ILogger<HomeController> logger,IBlobService blob)
    {
        _logger = logger;
        _blob = blob;
    }

    public IActionResult Index()
    {
        return View();
    }

    public  IActionResult Images()
    {
        return View(_blob.GetAllBlobsByUri("dotnet-images").GetAwaiter().GetResult());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
