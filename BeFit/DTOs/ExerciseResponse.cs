using BeFit.Models.Enums;

namespace BeFit.DTOs;

public record ExerciseResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public ExerciseType Category { get; init; }
    public MuscleType TargetMuscle { get; init; }
    public Difficulty Difficulty { get; init; }
    public string Instructions { get; init; } = string.Empty;
    public string? Equipment { get; init; }
}