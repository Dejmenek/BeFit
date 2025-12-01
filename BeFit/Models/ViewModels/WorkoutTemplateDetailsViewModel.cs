using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class WorkoutTemplateDetailsViewModel
{
    public WorkoutTemplateResponse Template { get; set; } = null!;
    public List<WorkoutTemplateExerciseWithExerciseNameResponse> Exercises { get; set; } = new();
}