using BeFit.DTOs;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize(Roles = "Admin")]
public class ExercisesController : Controller
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var result = await _exerciseService.GetExercisesAsync(pageNumber, 10);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return View(result.Value);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExerciseRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var result = await _exerciseService.CreateExerciseAsync(request);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index));
    }

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
    public async Task<IActionResult> Edit(int id, ExerciseRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var result = await _exerciseService.UpdateExerciseAsync(id, request);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _exerciseService.RemoveSingleExerciseAsync(id);
        if (!result.IsSuccess)
            return View("Error", result.Error);

        return RedirectToAction(nameof(Index));
    }
}
