using BeFit.DTOs;
using BeFit.Models.ViewModels;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class WorkoutTemplatesController : BaseController
{
    private readonly IWorkoutTemplateService _workoutTemplateService;
    private readonly IWorkoutTemplateExerciseService _workoutTemplateExerciseService;

    public WorkoutTemplatesController(IWorkoutTemplateService workoutTemplateService, IWorkoutTemplateExerciseService workoutTemplateExerciseService)
    {
        _workoutTemplateService = workoutTemplateService;
        _workoutTemplateExerciseService = workoutTemplateExerciseService;
    }

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var userId = GetUserId();
        var result = await _workoutTemplateService.GetUserWorkoutTemplatesAsync(userId!, pageNumber, 10);
        if (!result.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        return View(result.Value);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WorkoutTemplateRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var userId = GetUserId();
        var result = await _workoutTemplateService.CreateWorkoutTemplateAsync(userId!, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout template created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = GetUserId();
        var templateResult = await _workoutTemplateService.GetWorkoutTemplateByIdAsync(userId!, id);
        if (!templateResult.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        var exercisesResult = await _workoutTemplateExerciseService.GetWorkoutTemplateExercisesWithNamesAsync(userId!, id);
        if (!exercisesResult.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        var viewModel = new WorkoutTemplateDetailsViewModel
        {
            Template = templateResult.Value,
            Exercises = exercisesResult.Value
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = GetUserId();
        var result = await _workoutTemplateService.GetWorkoutTemplateByIdAsync(userId, id);
        if (!result.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        var request = new WorkoutTemplateRequest
        {
            Name = result.Value.Name,
            Description = result.Value.Description,
            Goals = result.Value.Goals,
            PreferredDay = result.Value.PreferredDay
        };

        ViewBag.Id = id;
        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WorkoutTemplateRequest request)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Id = id;
            return View(request);
        }

        var userId = GetUserId();
        var result = await _workoutTemplateService.UpdateWorkoutTemplateAsync(userId, id, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout template updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = GetUserId();
        var result = await _workoutTemplateService.DeleteWorkoutTemplateAsync(userId, id);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout template deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
