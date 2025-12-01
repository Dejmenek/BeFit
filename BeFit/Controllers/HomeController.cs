using System.Diagnostics;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers;

public class HomeController : Controller
{
    private readonly IExerciseService _exerciseService;

    public HomeController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> Index()
    {
        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        var exercises = exercisesResult.IsSuccess ? exercisesResult.Value : new List<ExerciseResponse>();
        
        ViewBag.Exercises = exercises.Take(6).ToList();
        return View();
    }

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
