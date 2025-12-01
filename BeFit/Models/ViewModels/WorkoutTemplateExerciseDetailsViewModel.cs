using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class WorkoutTemplateExerciseDetailsViewModel
{
    public WorkoutTemplateExerciseWithExerciseNameResponse Exercise { get; set; } = null!;
    public int WorkoutTemplateId { get; set; }
}