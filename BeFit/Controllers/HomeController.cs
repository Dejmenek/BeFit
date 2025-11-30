using System.Diagnostics;
using BeFit.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index() => View();

    public IActionResult About() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
