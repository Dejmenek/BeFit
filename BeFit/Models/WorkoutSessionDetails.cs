namespace BeFit.Models;

public class WorkoutSessionDetails
{
    public int Id { get; set; }
    public int WorkoutSessionId { get; set; }
    public int ExerciseId { get; set; }
    public int? Sets { get; set; }
    public int? Repetitions { get; set; }
    public decimal? Weight { get; set; }
    public int RestTimeInSeconds { get; set; }
    public string? Tempo { get; set; }
    public int? DurationInSeconds { get; set; }
    public decimal? Distance { get; set; }

    public WorkoutSession WorkoutSession { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
