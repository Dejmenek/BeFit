using BeFit.DTOs;
using BeFit.Models.ViewModels;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class WorkoutTemplateExercisesController : BaseController
{
    private readonly IWorkoutTemplateExerciseService _workoutTemplateExerciseService;
    private readonly IExerciseService _exerciseService;

    public WorkoutTemplateExercisesController(IWorkoutTemplateExerciseService workoutTemplateExerciseService, IExerciseService exerciseService)
    {
        _workoutTemplateExerciseService = workoutTemplateExerciseService;
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> Create(int workoutTemplateId)
    {
        var userId = GetUserId();

        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
        {
            TempData["Error"] = exercisesResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        var viewModel = new WorkoutTemplateExerciseCreateViewModel
        {
            Exercises = exercisesResult.Value,
            WorkoutTemplateId = workoutTemplateId
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int workoutTemplateId, [Bind(Prefix = "Request")] WorkoutTemplateExerciseRequest request)
    {
        if (!ModelState.IsValid)
        {
            var exercisesResult = await _exerciseService.GetAllExercisesAsync();
            if (!exercisesResult.IsSuccess)
            {
                TempData["Error"] = exercisesResult.Error.Description;
                return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
            }

            var viewModel = new WorkoutTemplateExerciseCreateViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutTemplateId = workoutTemplateId
            };

            return View(viewModel);
        }

        var userId = GetUserId();
        var requestWithTemplateId = request with { WorkoutTemplateId = workoutTemplateId };
        var result = await _workoutTemplateExerciseService.AddWorkoutTemplateExerciseAsync(userId!, workoutTemplateId, requestWithTemplateId);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        TempData["Success"] = "Workout template exercise added successfully.";
        return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
    }

    public async Task<IActionResult> Edit(int id, int workoutTemplateId)
    {
        var userId = GetUserId();
        var exerciseResult = await _workoutTemplateExerciseService.GetWorkoutTemplateExerciseByIdAsync(userId!, id);
        if (!exerciseResult.IsSuccess)
        {
            TempData["Error"] = exerciseResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        var exercisesResult = await _exerciseService.GetAllExercisesAsync();
        if (!exercisesResult.IsSuccess)
        {
            TempData["Error"] = exercisesResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        var request = new WorkoutTemplateExerciseRequest
        {
            WorkoutTemplateId = exerciseResult.Value.WorkoutTemplateId,
            ExerciseId = exerciseResult.Value.ExerciseId,
            Order = exerciseResult.Value.Order,
            TargetSets = exerciseResult.Value.TargetSets,
            TargetReps = exerciseResult.Value.TargetReps,
            TargetWeight = exerciseResult.Value.TargetWeight,
            RestTimeInSeconds = exerciseResult.Value.RestTimeInSeconds,
            Tempo = exerciseResult.Value.Tempo,
            TargetDistance = exerciseResult.Value.TargetDistance,
            TargetDurationInSeconds = exerciseResult.Value.TargetDurationInSeconds
        };

        var viewModel = new WorkoutTemplateExerciseEditViewModel
        {
            Request = request,
            Exercises = exercisesResult.Value,
            WorkoutTemplateId = workoutTemplateId,
            Id = id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, int workoutTemplateId, [Bind(Prefix = "Request")] WorkoutTemplateExerciseRequest request)
    {
        if (!ModelState.IsValid)
        {
            var exercisesResult = await _exerciseService.GetAllExercisesAsync();
            if (!exercisesResult.IsSuccess)
            {
                TempData["Error"] = exercisesResult.Error.Description;
                return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
            }

            var viewModel = new WorkoutTemplateExerciseEditViewModel
            {
                Request = request,
                Exercises = exercisesResult.Value,
                WorkoutTemplateId = workoutTemplateId,
                Id = id
            };

            return View(viewModel);
        }

        var userId = GetUserId();
        var requestWithTemplateId = request with { WorkoutTemplateId = workoutTemplateId };
        var result = await _workoutTemplateExerciseService.UpdateWorkoutTemplateExerciseAsync(userId!, id, requestWithTemplateId);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        TempData["Success"] = "Workout template exercise updated successfully.";
        return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
    }

    public async Task<IActionResult> Details(int id, int workoutTemplateId)
    {
        var userId = GetUserId();
        var exerciseResult = await _workoutTemplateExerciseService.GetWorkoutTemplateExerciseByIdAsync(userId!, id);
        if (!exerciseResult.IsSuccess)
        {
            TempData["Error"] = exerciseResult.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        var viewModel = new WorkoutTemplateExerciseDetailsViewModel
        {
            Exercise = exerciseResult.Value,
            WorkoutTemplateId = workoutTemplateId
        };

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int workoutTemplateId)
    {
        var userId = GetUserId();
        var result = await _workoutTemplateExerciseService.RemoveWorkoutTemplateExerciseAsync(userId!, id);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
        }

        TempData["Success"] = "Workout template exercise deleted successfully.";
        return RedirectToAction(nameof(Details), "WorkoutTemplates", new { id = workoutTemplateId });
    }
}
