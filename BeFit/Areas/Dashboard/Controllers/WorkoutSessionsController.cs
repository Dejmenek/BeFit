using BeFit.DTOs;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class WorkoutSessionsController : BaseController
{
    private readonly IWorkoutSessionService _workoutSessionService;
    private readonly IWorkoutSessionDetailsService _workoutSessionDetailsService;

    public WorkoutSessionsController(IWorkoutSessionService workoutSessionService, IWorkoutSessionDetailsService workoutSessionDetailsService)
    {
        _workoutSessionService = workoutSessionService;
        _workoutSessionDetailsService = workoutSessionDetailsService;
    }

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var userId = GetUserId();
        var result = await _workoutSessionService.GetUserWorkoutSessionsAsync(userId!, pageNumber, 5);
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
    public async Task<IActionResult> Create(WorkoutSessionRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var userId = GetUserId();

        var result = await _workoutSessionService.CreateWorkoutSessionAsync(userId!, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout session created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = GetUserId();
        var sessionResult = await _workoutSessionService.GetWorkoutSessionByIdAsync(userId!, id);
        if (!sessionResult.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        var detailsResult = await _workoutSessionDetailsService.GetWorkoutSessionDetailsAsync(userId!, id);
        if (!detailsResult.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        var viewModel = new WorkoutSessionDetailsViewModel
        {
            Session = sessionResult.Value,
            Details = detailsResult.Value
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = GetUserId();
        var result = await _workoutSessionService.GetWorkoutSessionByIdAsync(userId!, id);
        if (!result.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WorkoutSessionRequest request)
    {
        if (!ModelState.IsValid)
        {
            var response = new WorkoutSessionResponse
            {
                Id = id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Notes = request.Notes
            };
            return View(response);
        }

        var userId = GetUserId();
        var result = await _workoutSessionService.UpdateWorkoutSessionAsync(userId!, id, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout session updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = GetUserId();
        var result = await _workoutSessionService.DeleteWorkoutSessionAsync(userId!, id);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Workout session deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
