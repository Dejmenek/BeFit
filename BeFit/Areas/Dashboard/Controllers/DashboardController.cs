using BeFit.Models;
using BeFit.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize]
public class DashboardController : BaseController
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(
        IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        var workoutStatsResult = await _dashboardService.GetWorkoutStatsAsync(userId!);
        var exerciseStatsResult = await _dashboardService.GetExerciseStatsAsync(userId!);
        var trainingCalendarResult = await _dashboardService.GetTrainingCalendarAsync(userId!);

        if (!exerciseStatsResult.IsSuccess || !trainingCalendarResult.IsSuccess || !workoutStatsResult.IsSuccess)
        {
            return View("Error");
        }

        var vm = new DashboardViewModel
        {
            WorkoutStats = workoutStatsResult.Value,
            ExerciseStats = exerciseStatsResult.Value,
            TrainingCalendar = trainingCalendarResult.Value
        };

        return View(vm);
    }
}
