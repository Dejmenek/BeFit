using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutSessionDetailRequest
{
    [Required]
    [Display(Name = "Exercise")]
    public int ExerciseId { get; init; }

    [Range(1, 10)]
    [Display(Name = "Sets")]
    public int? Sets { get; init; }

    [Range(1, 100)]
    [Display(Name = "Repetitions")]
    public int? Repetitions { get; init; }

    [Range(0, 1000)]
    [Display(Name = "Weight (kg)")]
    public decimal? Weight { get; init; }

    [Range(0, 1800)]
    [Display(Name = "Rest Time (seconds)")]
    public int RestTimeInSeconds { get; init; }

    [StringLength(32)]
    [Display(Name = "Tempo")]
    public string? Tempo { get; init; }

    [Range(0, 36000)]
    [Display(Name = "Duration (seconds)")]
    public int? DurationInSeconds { get; init; }

    [Range(0, 100)]
    [Display(Name = "Distance (km)")]
    public decimal? Distance { get; init; }
}