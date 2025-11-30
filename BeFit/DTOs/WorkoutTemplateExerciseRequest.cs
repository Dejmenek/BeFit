using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutTemplateExerciseRequest
{
    public int WorkoutTemplateId { get; init; }

    [Required]
    [Display(Name = "Exercise")]
    public int ExerciseId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Order must be greater than 0")]
    [Display(Name = "Order")]
    public int Order { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Target sets must be 0 or greater")]
    [Display(Name = "Target Sets")]
    public int? TargetSets { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Target reps must be 0 or greater")]
    [Display(Name = "Target Reps")]
    public int? TargetReps { get; init; }

    [Range(0, 1000, ErrorMessage = "Target weight must be between 0 and 1000 kg")]
    [Display(Name = "Target Weight (kg)")]
    public decimal? TargetWeight { get; init; }

    [Required]
    [Range(0, 1800, ErrorMessage = "Rest time must be between 0 and 1800 seconds")]
    [Display(Name = "Rest Time (seconds)")]
    public int RestTimeInSeconds { get; init; }

    [StringLength(32, ErrorMessage = "Tempo cannot exceed 32 characters")]
    [Display(Name = "Tempo")]
    public string? Tempo { get; init; }

    [Range(0, 100, ErrorMessage = "Target distance must be between 0 and 100 km")]
    [Display(Name = "Target Distance (km)")]
    public decimal? TargetDistance { get; init; }

    [Range(0, 36000, ErrorMessage = "Target duration must be between 0 and 36000 seconds")]
    [Display(Name = "Target Duration (seconds)")]
    public int? TargetDurationInSeconds { get; init; }
}
