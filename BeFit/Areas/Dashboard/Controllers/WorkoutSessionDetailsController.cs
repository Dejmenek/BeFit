using BeFit.DTOs;
using BeFit.Models.ViewModels;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class WorkoutSessionDetailsController : BaseController
{
    private readonly IWorkoutSessionDetailsService _workoutSessionDetailsService;
    private readonly IExerciseService _exerciseService;

    public WorkoutSessionDetailsController(IWorkoutSessionDetailsService workoutSessionDetailsService, IExerciseService exerciseService)
    {
        _workoutSessionDetailsService = workoutSessionDetailsService;
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> Create(int workoutSessionId)
    {
        var userId = GetUserId();

        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
        {
            TempData["Error"] = exercisesResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        var viewModel = new WorkoutSessionDetailCreateViewModel
        {
            Exercises = exercisesResult.Value,
            WorkoutSessionId = workoutSessionId
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int workoutSessionId, [Bind(Prefix = "Request")] WorkoutSessionDetailRequest request)
    {
        if (!ModelState.IsValid)
        {
            var exercisesResult = await _exerciseService.GetAllExercisesAsync();
            if (!exercisesResult.IsSuccess)
            {
                TempData["Error"] = exercisesResult.Error.Description;
                return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
            }

            var viewModel = new WorkoutSessionDetailCreateViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutSessionId = workoutSessionId
            };

            return View(viewModel);
        }

        var userId = GetUserId();
        var result = await _workoutSessionDetailsService.AddWorkoutSessionDetailAsync(userId!, workoutSessionId, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        TempData["Success"] = "Workout session detail added successfully.";
        return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
    }

    public async Task<IActionResult> Edit(int id, int workoutSessionId)
    {
        var userId = GetUserId();
        var detailResult = await _workoutSessionDetailsService.GetWorkoutSessionDetailsByIdAsync(userId!, id);
        if (!detailResult.IsSuccess)
        {
            TempData["Error"] = detailResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
        {
            TempData["Error"] = exercisesResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        var request = new WorkoutSessionDetailRequest
        {
            ExerciseId = detailResult.Value.ExerciseId,
            Sets = detailResult.Value.Sets,
            Repetitions = detailResult.Value.Repetitions,
            Weight = detailResult.Value.Weight,
            RestTimeInSeconds = detailResult.Value.RestTimeInSeconds,
            Tempo = detailResult.Value.Tempo,
            DurationInSeconds = detailResult.Value.DurationInSeconds,
            Distance = detailResult.Value.Distance
        };

        var viewModel = new WorkoutSessionDetailEditViewModel
        {
            Request = request,
            Exercises = exercisesResult.Value,
            WorkoutSessionId = workoutSessionId,
            Id = id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, int workoutSessionId, [Bind(Prefix = "Request")] WorkoutSessionDetailRequest request)
    {
        if (!ModelState.IsValid)
        {
            var exercisesResult = await _exerciseService.GetAllExercisesAsync();
            if (!exercisesResult.IsSuccess)
            {
                TempData["Error"] = exercisesResult.Error.Description;
                return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
            }

            var viewModel = new WorkoutSessionDetailEditViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutSessionId = workoutSessionId,
                Id = id
            };

            return View(viewModel);
        }

        var userId = GetUserId();
        var result = await _workoutSessionDetailsService.UpdateWorkoutSessionDetailAsync(userId!, id, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        TempData["Success"] = "Workout session detail updated successfully.";
        return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
    }

    public async Task<IActionResult> Details(int id, int workoutSessionId)
    {
        var userId = GetUserId();
        var detailResult = await _workoutSessionDetailsService.GetWorkoutSessionDetailsByIdAsync(userId!, id);
        if (!detailResult.IsSuccess)
        {
            TempData["Error"] = detailResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        var viewModel = new WorkoutSessionDetailDetailsViewModel
        {
            Detail = detailResult.Value,
            WorkoutSessionId = workoutSessionId
        };

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int workoutSessionId)
    {
        var userId = GetUserId();
        var result = await _workoutSessionDetailsService.RemoveWorkoutSessionDetailAsync(userId!, id);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
        }

        TempData["Success"] = "Workout session detail deleted successfully.";
        return RedirectToAction(nameof(Details), "WorkoutSessions", new { id = workoutSessionId });
    }
}
