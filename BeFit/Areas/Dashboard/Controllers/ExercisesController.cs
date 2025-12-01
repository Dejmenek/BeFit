using BeFit.DTOs;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class ExercisesController : BaseController
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [Authorize]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var result = await _exerciseService.GetExercisesAsync(pageNumber, 10);
        if (!result.IsSuccess)
            return RedirectToAction("Error", "Home", new { area = "" });

        return View(result.Value);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ExerciseRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var result = await _exerciseService.CreateExerciseAsync(request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Exercise created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _exerciseService.GetExercisesAsync(1, int.MaxValue);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        var exercise = result.Value.Items.FirstOrDefault(e => e.Id == id);
        if (exercise == null)
            return NotFound();

        var request = new ExerciseRequest
        {
            Name = exercise.Name,
            Category = exercise.Category,
            TargetMuscle = exercise.TargetMuscle,
            Difficulty = exercise.Difficulty,
            Instructions = exercise.Instructions,
            Equipment = exercise.Equipment
        };

        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, ExerciseRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var result = await _exerciseService.UpdateExerciseAsync(id, request);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Exercise updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _exerciseService.RemoveSingleExerciseAsync(id);
        if (!result.IsSuccess)
        {
            TempData["Error"] = result.Error.Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "Exercise deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
