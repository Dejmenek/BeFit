using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class WorkoutTemplateExerciseEditViewModel
{
    public WorkoutTemplateExerciseRequest Request { get; set; } = new();
    public List<ExerciseResponse> Exercises { get; set; } = new();
    public int WorkoutTemplateId { get; set; }
    public int Id { get; set; }
}