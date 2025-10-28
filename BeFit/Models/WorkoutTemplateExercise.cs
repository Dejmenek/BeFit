namespace BeFit.Models;

public class WorkoutTemplateExercise
{
    public int Id { get; set; }
    public int WorkoutTemplateId { get; set; }
    public int ExerciseId { get; set; }
    public int Order { get; set; }
    public int? TargetSets { get; set; }
    public int? TargetReps { get; set; }
    public decimal? TargetWeight { get; set; }
    public int RestTimeInSeconds { get; set; }
    public string? Tempo { get; set; }
    public decimal? TargetDistance { get; set; }
    public int? TargetDurationInSeconds { get; set; }

    public WorkoutTemplate WorkoutTemplate { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
