using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class WorkoutTemplateExerciseCreateViewModel
{
    public WorkoutTemplateExerciseRequest Request { get; set; } = new();
    public List<ExerciseResponse> Exercises { get; set; } = new();
    public int WorkoutTemplateId { get; set; }
}