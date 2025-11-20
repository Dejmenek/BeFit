using BeFit.DTOs;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class WorkoutSessionDetailsController : Controller
{
    private readonly IWorkoutSessionDetailsService _workoutSessionDetailsService;
    private readonly IExerciseService _exerciseService;

    public WorkoutSessionDetailsController(IWorkoutSessionDetailsService workoutSessionDetailsService, IExerciseService exerciseService)
    {
        _workoutSessionDetailsService = workoutSessionDetailsService;
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> Index(int workoutSessionId)
    {
        var result = await _workoutSessionDetailsService.GetWorkoutSessionDetailsAsync(workoutSessionId);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return View(result.Value);
    }

    public async Task<IActionResult> Create(int workoutSessionId)
    {
        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
            return View("Error", exercisesResult.Error);

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
                return View("Error", exercisesResult.Error);

            var viewModel = new WorkoutSessionDetailCreateViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutSessionId = workoutSessionId
            };

            return View(viewModel);
        }

        var result = await _workoutSessionDetailsService.AddWorkoutSessionDetailAsync(workoutSessionId, request);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index), new { workoutSessionId });
    }

    public async Task<IActionResult> Edit(int id, int workoutSessionId)
    {
        var detailResult = await _workoutSessionDetailsService.GetWorkoutSessionDetailsByIdAsync(id);
        if (!detailResult.IsSuccess)
            return View("Error", detailResult.Error);

        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
            return View("Error", exercisesResult.Error);

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
                return View("Error", exercisesResult.Error);

            var viewModel = new WorkoutSessionDetailEditViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutSessionId = workoutSessionId,
                Id = id
            };

            return View(viewModel);
        }

        var result = await _workoutSessionDetailsService.UpdateWorkoutSessionDetailAsync(id, request);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index), new { workoutSessionId });
    }

    public async Task<IActionResult> Details(int id, int workoutSessionId)
    {
        var detailResult = await _workoutSessionDetailsService.GetWorkoutSessionDetailsByIdAsync(id);
        if (!detailResult.IsSuccess)
            return View("Error", detailResult.Error);

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
        var result = await _workoutSessionDetailsService.RemoveWorkoutSessionDetailAsync(id);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index), new { workoutSessionId });
    }
}
