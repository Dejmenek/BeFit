using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IDashboardService
{
    Task<Result<WorkoutStatsResponse>> GetWorkoutStatsAsync(string userId);
    Task<Result<List<ExerciseStatsResponse>>> GetExerciseStatsAsync(string userId);
    Task<Result<List<WorkoutTemplateCalendarResponse>>> GetTrainingCalendarAsync(string userId);
}
