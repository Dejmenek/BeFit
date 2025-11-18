using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutSessionDetailRequest
{
    [Required]
    public int ExerciseId { get; init; }

    [Range(1, 10)]
    public int? Sets { get; init; }

    [Range(1, 100)]
    public int? Repetitions { get; init; }

    [Range(0, 1000)]
    public decimal? Weight { get; init; }

    [Range(0, 1800)]
    public int RestTimeInSeconds { get; init; }

    [StringLength(32)]
    public string? Tempo { get; init; }

    [Range(0, 36000)]
    public int? DurationInSeconds { get; init; }

    [Range(0, 100)]
    public decimal? Distance { get; init; }
}