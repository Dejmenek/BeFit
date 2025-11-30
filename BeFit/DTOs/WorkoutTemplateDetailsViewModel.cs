namespace BeFit.DTOs;

public class WorkoutTemplateDetailsViewModel
{
    public WorkoutTemplateResponse Template { get; set; } = null!;
    public List<WorkoutTemplateExerciseWithExerciseNameResponse> Exercises { get; set; } = new();
}

