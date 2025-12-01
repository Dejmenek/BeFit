using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class DashboardViewModel
{
    public WorkoutStatsResponse WorkoutStats { get; set; } = null!;
    public List<ExerciseStatsResponse> ExerciseStats { get; set; } = new();
    public List<WorkoutTemplateCalendarResponse> TrainingCalendar { get; set; } = new();
}