namespace BeFit.DTOs;

public class WorkoutTemplateExerciseCreateViewModel
{
    public WorkoutTemplateExerciseRequest Request { get; set; } = new();
    public List<ExerciseResponse> Exercises { get; set; } = new();
    public int WorkoutTemplateId { get; set; }
}

